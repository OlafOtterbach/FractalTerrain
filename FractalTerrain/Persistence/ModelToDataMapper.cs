/// <summary>Definition of the class ModelToDataMapper.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.ViewModel;

namespace FractalTerrain.Persistence
{
   public class ModelToDataMapper
   {
      public DataV1Dot1 Map( TerrainModel model, ViewModelSettings settings )
      {
         var data = new DataV1Dot1
         {
            MapSize = model.MapSize,
            AppleManMinimalSize = model.AppleManMinimalSize,
            AppleManMinimalPosition = model.AppleManMinimalPosition,
            AppleManMaximalPosition = model.AppleManMaximalPosition,
            AppleManXStartPosition = model.AppleManXStartPosition,
            AppleManYStartPosition = model.AppleManYStartPosition,
            AppleManSize = model.AppleManSize,
            HoricontalRatio = settings.HoricontalRatio,
            VerticalRatio = settings.VerticalRatio,
            CameraTopLeft = settings.CameraTopLeft,
            CameraTopRight = settings.CameraTopRight,
            CameraBottomLeft = settings.CameraBottomLeft,
            CameraBottomRight = settings.CameraBottomRight,
            CameraSetting = settings.CameraSetting,
         }; 
         return data;
      }
   }
}
