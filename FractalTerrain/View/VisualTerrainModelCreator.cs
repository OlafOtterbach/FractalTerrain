using FractalTerrain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FractalTerrain.View
{
   static class VisualTerrainModelCreator
   {
      public static VisualTerrainModel CreateVisualModel(TerrainModel model)
      {
         var terrainModel = model.TerrainModelData;
         var visualTerrainModel = new VisualTerrainModel(100.0);
         CreateVisualVertices(terrainModel, visualTerrainModel);
         CreateBoundedBox(visualTerrainModel);
         CreateVisualLines( terrainModel,  visualTerrainModel);
         ColorizeModel( terrainModel,  visualTerrainModel);
         return visualTerrainModel;
      }

      private static void CreateVisualVertices(TerrainModelData terrainModel, VisualTerrainModel visualTerrainModel)
      {
         var vertices = new VisualVertex[terrainModel.Size * terrainModel.Size];
         var height = visualTerrainModel.HeightFactor;
         var size = 100.0;
         var step = size / (terrainModel.Size - 1);
         Parallel.For(0, terrainModel.Size, y =>
         {
            for (var x = 0; x < terrainModel.Size; x++)
            {
               var index = x + y * terrainModel.Size;
               vertices[index] = new VisualVertex();
               vertices[index].Vertex = new Point3D(x * step - size / 2.0, y * step - size / 2.0, terrainModel.Terrain[x, y] * height);
            }
         });
         visualTerrainModel.Size = size;
         visualTerrainModel.Vertices = vertices;
      }

      private static void CreateBoundedBox(VisualTerrainModel visualTerrainModel)
      {
         var vertices = visualTerrainModel.Vertices;
         visualTerrainModel.Minimum = new Point3D(vertices.AsParallel().Select(p => p.Vertex.X).Min(), vertices.AsParallel().Select(p => p.Vertex.Y).Min(), vertices.AsParallel().Select(p => p.Vertex.Z).Min());
         visualTerrainModel.Maximum = new Point3D(vertices.AsParallel().Select(p => p.Vertex.X).Max(), vertices.AsParallel().Select(p => p.Vertex.Y).Max(), vertices.AsParallel().Select(p => p.Vertex.Z).Max());
      }

      private static void CreateVisualLines(TerrainModelData model, VisualTerrainModel visualTerrainModel)
      {
         var size = model.Size;
         var vertices = visualTerrainModel.Vertices;
         Parallel.For(0, size, y =>
         {
            for (var x = 0; x < size; x++)
            {
               var iPoint = x + y * size;
               if (x < size - 1)
               {
                  var iRightPoint = iPoint + 1;
                  var line = new VisualLine(vertices[iPoint].Vertex, vertices[iRightPoint].Vertex);
                  vertices[iPoint].East = line;
                  vertices[iRightPoint].West = line;
               }
               if (y < size - 1)
               {
                  var iUpPoint = iPoint + size;
                  var line = new VisualLine(vertices[iPoint].Vertex, vertices[iUpPoint].Vertex);
                  vertices[iPoint].North = line;
                  vertices[iUpPoint].South = line;
               }
            }
         });
      }

      private static void ColorizeModel(TerrainModelData model, VisualTerrainModel visualTerrainModel)
      {
         var maxZ = visualTerrainModel.Maximum.Z;
         if (maxZ < visualTerrainModel.HeightFactor / 3.0 * 0.8) maxZ = visualTerrainModel.HeightFactor / 3.0 * 0.8;
         var minZ = visualTerrainModel.Minimum.Z;
         var delta = (maxZ - minZ);
         var blueLimit = minZ + delta * 0.2;
         var grayLimit = maxZ - delta * 0.3;
         var whiteLimit = maxZ - delta * 0.2;
         var lines = visualTerrainModel.Vertices.SelectMany(v => new List<VisualLine>() { v.South, v.West }).Where(x => x != null).ToList();
         lines.AsParallel().Where(line => line.Start.Z <= blueLimit || line.End.Z <= blueLimit).ToList().ForEach(x => x.SetPen(0, 0, 255));
         lines.AsParallel().Where(line => (line.Start.Z > blueLimit && line.End.Z > blueLimit)
                             && (line.Start.Z < grayLimit && line.End.Z < grayLimit)).ToList()
              .ForEach(x => x.SetPen(0, 255, 0));
         lines.AsParallel().Where(line => (line.Start.Z > grayLimit && line.End.Z > grayLimit)
                             && (line.Start.Z < whiteLimit && line.End.Z < whiteLimit)).ToList()
              .ForEach(x => x.SetPen(128, 128, 128));
         lines.AsParallel().Where(line => line.Start.Z >= whiteLimit || line.End.Z >= whiteLimit).ToList().ForEach(x => x.SetPen(255, 255, 255));
      }
   }
}
