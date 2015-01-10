/// <summary>Definition of the class ConverterV1ToV1Dot1.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.ViewModel;
namespace FractalTerrain.Persistence
{
   public class ConverterV1ToV1Dot1 : Converter
   {
      public override Context Convert( Context ctx )
      {
         var dataV1 = ctx.Data as DataV1;
         var dataV1DotNet = new DataV1Dot1
         {
            MapSize = dataV1.MapSize,
            AppleManXStartPosition = dataV1.AppleManXStartPosition,
            AppleManYStartPosition = dataV1.AppleManYStartPosition,
            AppleManSize = dataV1.AppleManSize,
            AppleManMinimalPosition = dataV1.AppleManMinimalPosition,
            AppleManMaximalPosition = dataV1.AppleManMaximalPosition,
            AppleManMinimalSize = dataV1.AppleManMinimalSize,
            CameraTopLeft = new CameraSettings(),
            CameraTopRight = new CameraSettings(),
            CameraBottomLeft = new CameraSettings(),
            CameraBottomRight = new CameraSettings(),
            CameraSetting = new CameraSettings(),
            HoricontalRatio = 1.0,
            VerticalRatio = 1.0
         };
         return ctx;
      }
   }
}
