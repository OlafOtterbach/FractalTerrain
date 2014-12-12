/// <summary>Definition of the class DataToModelMapper.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;

namespace FractalTerrain.Persistence
{
   public class DataToModelMapper
   {
      private MapperV1 _mapper;
      private TerrainModelBuilder _builder;

      public DataToModelMapper( MapperV1 mapper )
      {
         _mapper = mapper;
         _builder = new TerrainModelBuilder();
      }

      public Context Map( Context ctx )
      {
         var data = _mapper.Map( ctx ).Data as DataV1;
         ctx.Model = _builder.Create( data.MapSize,
                                      data.AppleManMinimalSize,
                                      data.AppleManMinimalPosition,
                                      data.AppleManMaximalPosition,
                                      data.AppleManXStartPosition,
                                      data.AppleManYStartPosition,
                                      data.AppleManSize );
         return ctx;
      }
   }
}
