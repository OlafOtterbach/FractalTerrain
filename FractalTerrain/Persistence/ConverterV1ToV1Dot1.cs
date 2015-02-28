/// <summary>Definition of the class ConverterV1ToV1Dot1.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.ViewModel;

namespace FractalTerrain.Persistence
{
   public class ConverterV1ToV1Dot1 : Converter
   {
      public override Context Convert( Context ctx )
      {
         if (ctx == null)
         {
            ctx = new Context()
            {
               Data = new DataV1()
            };
         }
         var dataV1 = ctx.Data as DataV1;
         if (dataV1 != null)
         {
            ctx.Data = new DataV1Dot1
            {
               MapSize = dataV1.MapSize,
               AppleManXStartPosition = dataV1.AppleManXStartPosition,
               AppleManYStartPosition = dataV1.AppleManYStartPosition,
               AppleManSize = dataV1.AppleManSize,
               AppleManMinimalPosition = dataV1.AppleManMinimalPosition,
               AppleManMaximalPosition = dataV1.AppleManMaximalPosition,
               AppleManMinimalSize = dataV1.AppleManMinimalSize,
               CameraTopLeft = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 },
               CameraTopRight = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 },
               CameraBottomLeft = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 },
               CameraBottomRight = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 },
               CameraSetting = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 },
               HoricontalRatio = 1.0,
               VerticalRatio = 1.0
            };
         }
         return ctx;
      }
   }
}
