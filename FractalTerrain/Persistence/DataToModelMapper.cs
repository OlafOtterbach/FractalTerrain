/// <summary>Definition of the class DataToModelMapper.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.ViewModel;

namespace FractalTerrain.Persistence
{
   public class DataToModelMapper
   {
      private MapperV1Dot1 _mapper;
      private TerrainModelBuilder _builder;

      public DataToModelMapper( MapperV1Dot1 mapper )
      {
         _mapper = mapper;
         _builder = new TerrainModelBuilder();
      }

      public Context Map( Context ctx )
      {
         ctx = _mapper.Map( ctx );
         if( ctx.Rating.HasMappingError )
         {
            ctx.Rating.HasNoModel = true;
         }
         else
         {
            var data = ctx.Data as DataV1Dot1;
            ctx.Model = _builder.Create( data.MapSize,
                                         data.AppleManMinimalSize,
                                         data.AppleManMinimalPosition,
                                         data.AppleManMaximalPosition,
                                         data.AppleManXStartPosition,
                                         data.AppleManYStartPosition,
                                         data.AppleManSize );
            ctx.Setting = new ViewModelSettings
                          {
                             CameraTopLeft = data.CameraTopLeft,
                             CameraTopRight = data.CameraTopRight,
                             CameraBottomLeft = data.CameraBottomLeft,
                             CameraBottomRight = data.CameraBottomRight,
                             CameraSetting = data.CameraSetting,
                             HoricontalRatio = data.HoricontalRatio,
                             VerticalRatio = data.VerticalRatio
                          };
         }
         return ctx;
      }
   }
}
