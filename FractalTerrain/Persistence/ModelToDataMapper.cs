/// <summary>Definition of the class ModelToDataMapper.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;

namespace FractalTerrain.Persistence
{
   public class ModelToDataMapper
   {
      public DataV1 Map( TerrainModel model)
      {
         var data = new DataV1
         {
            MapSize = model.MapSize,
            AppleManMinimalSize = model.AppleManMinimalSize,
            AppleManMinimalPosition = model.AppleManMinimalPosition,
            AppleManMaximalPosition = model.AppleManMaximalPosition,
            AppleManXStartPosition = model.AppleManXStartPosition,
            AppleManYStartPosition = model.AppleManYStartPosition,
            AppleManSize = model.AppleManSize
         };
         return data;
      }
   }
}
