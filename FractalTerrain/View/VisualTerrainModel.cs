/// <summary>Definition of the class VisualModel.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FractalTerrain.View
{
   public class VisualTerrainModel
   {
      public VisualTerrainModel( double heightFactor)
      {
         HeightFactor = heightFactor;
      }

      public Point3D Minimum { get; set; }

      public Point3D Maximum { get; set; }

      public double HeightFactor { get; set; }

      public bool IsValid { get { return ( Vertices != null ); } }

      public VisualVertex[] Vertices{get; set;}

      public double Size{ get; set;}

      public IEnumerable<VisualLine> GetGeometryLines( ViewCamera camera )
      {
         var cameraOffset = camera.Offset.Offset();
         var offset = new Point3D(cameraOffset.X, cameraOffset.Y, 0.0);
         var max = Size - 1;
         var positions = new List<VectorInt>() { new VectorInt(0, 0), new VectorInt(max, 0), new VectorInt(0, max), new VectorInt(max, max) };
         var positionAndVertex = positions.Select(pos => new { Position = pos, Vertex = Vertices[pos.Y * Size + pos.X].Vertex }).ToList();
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
         var count = Size * Size * 2;
         var lines = new VisualLine[count];
         Parallel.For( 0, Size, y =>
         {
            var pos = start + y * yDirectionVector;
            for( int x = 0; x < Size; x++ )
            {
               var index = pos.Y * Size + pos.X;
               var lineIndex = 2 * (y * Size + x );
               lines[lineIndex] = ( Vertices[index].GetLineOfDirection(xDirection) );
               lines[lineIndex + 1] = ( Vertices[index].GetLineOfDirection(yDirection) );
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
   }
}
