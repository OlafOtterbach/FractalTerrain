/// <summary>Definition of the class MapperV1Dot1.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.ViewModel;
namespace FractalTerrain.Persistence
{
   public class MapperV1Dot1 : Mapper
   {
      public MapperV1Dot1( Mapper succesor, Converter converter )
         : base( succesor, converter )
      { }

      public override bool CanHandle( Context ctx )
      {
         return ( ctx.ParserData != null ) ? ctx.ParserData.Version == "V1.1" : false;
      }

      protected override Context MapToData( Context ctx )
      {
         try
         {
            var mapData = new DataV1Dot1()
            {
               MapSize = ParseInt( ctx.ParserData.Data["Data"]["MapSize"] ),
               AppleManXStartPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManXStartPosition"] ),
               AppleManYStartPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManYStartPosition"] ),
               AppleManSize = ParseDouble( ctx.ParserData.Data["Data"]["AppleManSize"] ),
               AppleManMinimalPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManMinimalPosition"] ),
               AppleManMaximalPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManMaximalPosition"] ),
               AppleManMinimalSize = ParseDouble( ctx.ParserData.Data["Data"]["AppleManMinimalSize"] ),
               HoricontalRatio = ParseDouble( ctx.ParserData.Data["Settings"]["HoricontalRatio"] ),
               VerticalRatio = ParseDouble( ctx.ParserData.Data["Settings"]["VerticalRatio"] ),
               CameraTopLeft = CameraSettings.TryParse( ctx.ParserData.Data["Settings"]["CameraTopLeft"] ),
               CameraTopRight = CameraSettings.TryParse( ctx.ParserData.Data["Settings"]["CameraTopRight"] ),
               CameraBottomLeft = CameraSettings.TryParse( ctx.ParserData.Data["Settings"]["CameraBottomLeft"] ),
               CameraBottomRight = CameraSettings.TryParse( ctx.ParserData.Data["Settings"]["CameraBottomRight"] ),
               CameraSetting = CameraSettings.TryParse( ctx.ParserData.Data["Settings"]["CameraSetting"] ),
            };
            ctx.Data = mapData;
         }
         catch
         {
            ctx.Rating.HasCorruptedData = true;
         }
         return ctx;
      }
   }
}
