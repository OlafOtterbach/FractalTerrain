﻿/// <summary>Definition of the class VisualModel.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FractalTerrain.View
{
   public class VisualModel
   {
      public VisualModel()
      {
         HeightFactor = 100.0;
      }

      public Point3D Minimum { get; private set; }

      public Point3D Maximum { get; private set; }

      public double HeightFactor { get; set; }

      public bool IsValid { get { return ( m_vertices != null ); } }

      public void InitTerrain( TerrainModel model )
      {
         CreateVisualModel(model);
      }


      public IEnumerable<VisualLine> GetGeometryLines( ViewCamera camera )
      {
         var cameraOffset = camera.Offset.Offset();
         var offset = new Point3D(cameraOffset.X, cameraOffset.Y, 0.0);
         var max = m_size - 1;
         var positions = new List<VectorInt>() { new VectorInt(0, 0), new VectorInt(max, 0), new VectorInt(0, max), new VectorInt(max, max) };
         var positionAndVertex = positions.Select(pos => new { Position = pos, Vertex = m_vertices[pos.Y * m_size + pos.X].Vertex }).ToList();
         var positionAndDistance = positionAndVertex.Select(x => new { Position = x.Position, Distance = ( x.Vertex - offset ).Length }).OrderBy( x => x.Distance ).ToList();
         var start = positionAndDistance[0].Position;
         var xEnd = positionAndDistance[1].Position;
         var p3 = positionAndDistance[2].Position;
         var p4 = positionAndDistance[3].Position;
         var xDirection = ( xEnd - start ).Normalized;
         var yDirection = ( p3 - start ).Normalized;
         if( (yDirection.X != 0) && (yDirection.Y != 0))
         {
            yDirection = ( p4 - start ).Normalized;
         }
         return GetLines( start, xDirection, yDirection );
      }


      private IEnumerable<VisualLine> GetLines(VectorInt start, VectorInt xDirectionVector, VectorInt yDirectionVector)
      {
         var xDirection = GetDirection(xDirectionVector);
         var yDirection = GetDirection(yDirectionVector);
         var count = m_size * m_size * 2;
         var lines = new VisualLine[count];
         Parallel.For( 0, m_size, y =>
         {
            var pos = start + y * yDirectionVector;
            for( int x = 0; x < m_size; x++ )
            {
               var index = pos.Y * m_size + pos.X;
               var lineIndex = 2 * (y * m_size + x );
               lines[lineIndex] = ( m_vertices[index].GetLineOfDirection(xDirection) );
               lines[lineIndex + 1] = ( m_vertices[index].GetLineOfDirection(yDirection) );
               pos = pos + xDirectionVector;
            };
         });
         var validLines = lines.Where(x => x != null).ToList();
         return validLines;
      }


      private VisualVertex.Direction GetDirection( VectorInt directionVector )
      {
         var direction = VisualVertex.Direction.e_east;
         var x = directionVector.X;
         var y = directionVector.Y;
         if( x != 0 )
         {
            direction = ( x >= 1 ) ? VisualVertex.Direction.e_east : VisualVertex.Direction.e_west;
         }
         else
         {
            direction = ( y >= 1 ) ? VisualVertex.Direction.e_north : VisualVertex.Direction.e_south;
         }
         return direction;
      }

      private void CreateVisualModel(TerrainModel terrainModel)
      {
         CreateVisualVertices(terrainModel);
         CreateBoundedBox();
         CreateVisualLines();
         ColorizeModel();
      }

      private void CreateVisualVertices(TerrainModel terrainModel)
      {
         var model = terrainModel.TerrainModelData;
         m_size = model.Size;
         m_vertices = new VisualVertex[model.Size * model.Size];
         var height = HeightFactor;
         var size = 100.0;
         var step = size / (model.Size - 1);
         Parallel.For(0, model.Size, y =>
         {
            for( var x = 0; x < model.Size; x++ )
            {
               var index = x + y * model.Size;
               m_vertices[index] = new VisualVertex();
               m_vertices[index].Vertex = new Point3D(x * step - size / 2.0, y * step - size / 2.0, model.Terrain[x, y] * height);
            }
         });
      }

      private void CreateBoundedBox()
      {
         Minimum = new Point3D(m_vertices.AsParallel().Select(p => p.Vertex.X).Min(), m_vertices.AsParallel().Select(p => p.Vertex.Y).Min(), m_vertices.AsParallel().Select(p => p.Vertex.Z).Min());
         Maximum = new Point3D(m_vertices.AsParallel().Select(p => p.Vertex.X).Max(), m_vertices.AsParallel().Select(p => p.Vertex.Y).Max(), m_vertices.AsParallel().Select(p => p.Vertex.Z).Max());
      }

      private void CreateVisualLines()
      {
         Parallel.For( 0, m_size, y=>
         {
            for (var x = 0; x < m_size; x++)
            {
               var iPoint = x + y * m_size;
               if (x < m_size - 1)
               {
                  var iRightPoint = iPoint + 1;
                  var line = new VisualLine(m_vertices[iPoint].Vertex, m_vertices[iRightPoint].Vertex);
                  m_vertices[iPoint].East = line;
                  m_vertices[iRightPoint].West = line;
               }
               if (y < m_size - 1)
               {
                  var iUpPoint = iPoint + m_size;
                  var line = new VisualLine(m_vertices[iPoint].Vertex, m_vertices[iUpPoint].Vertex);
                  m_vertices[iPoint].North = line;
                  m_vertices[iUpPoint].South = line;
               }
            }
         });
      }

      private List<VisualLine> ColorizeModel()
      {
         var maxZ = Maximum.Z;
         if( maxZ < HeightFactor / 3.0 * 0.8 ) maxZ = HeightFactor / 3.0 * 0.8;
         var minZ = Minimum.Z;
         var delta = (maxZ - minZ);
         var blueLimit = minZ + delta * 0.2;
         var grayLimit = maxZ - delta * 0.3;
         var whiteLimit = maxZ - delta * 0.2;
         var lines = m_vertices.SelectMany(v => new List<VisualLine>() { v.South, v.West }).Where(x => x != null).ToList();
         lines.AsParallel().Where(line => line.Start.Z <= blueLimit || line.End.Z <= blueLimit).ToList().ForEach(x => x.SetPen(0, 0, 255));
         lines.AsParallel().Where(line => ( line.Start.Z > blueLimit && line.End.Z > blueLimit )
                             && (line.Start.Z < grayLimit && line.End.Z < grayLimit)).ToList()
              .ForEach(x => x.SetPen(0, 255, 0));
         lines.AsParallel().Where(line => ( line.Start.Z > grayLimit && line.End.Z > grayLimit )
                             && (line.Start.Z < whiteLimit && line.End.Z < whiteLimit)).ToList()
              .ForEach(x => x.SetPen(128, 128, 128));
         lines.AsParallel().Where(line => line.Start.Z >= whiteLimit || line.End.Z >= whiteLimit).ToList().ForEach(x => x.SetPen(255, 255, 255));
         return lines;
      }

      private VisualVertex[] m_vertices;

      private int m_size;
   }
}
