using FractalTerrain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalTerrain.Persistence
{
   public class PersistMapperV1
   {
      public PersistDataV1 MapModelToPersistenceData(TerrainModel model)
      {
         var data = new PersistDataV1()
         {
            MapSize = model.MapSize,
            AppleManSize = model.AppleManSize,
            AppleManXStartPosition = model.AppleManXStartPosition,
            AppleManYStartPosition = model.AppleManYStartPosition,
            AppleManMinimalSize = model.AppleManMinimalSize,
            AppleManMinimalPosition = model.AppleManMinimalPosition,
            AppleManMaximalPosition = model.AppleManMaximalPosition,
            AppleManMap = new double[model.MapSize * model.MapSize],
            TerrainMap = new double[model.MapSize * model.MapSize]
         };
         Parallel.For(0, model.MapSize, i =>
         {
            for( int j = 0; j < model.MapSize; j++ )
            {
               data.AppleManMap[i * model.MapSize + j] = model.AppleManData.Map[i,j];
            }
         });
         Parallel.For(0, model.MapSize, i =>
         {
            for( int j = 0; j < model.MapSize; j++ )
            {
               data.TerrainMap[i * model.MapSize + j] = model.TerrainModelData.Terrain[i, j];
            }
         });
         return data;
      }

      public TerrainModel PersistenceDataToMapModel(PersistDataV1 model)
      {
         return null;
      }
   }
}
