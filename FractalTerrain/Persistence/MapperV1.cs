/// <summary>Definition of the class MapperV1.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Persistence
{
   public class MapperV1 : Mapper
   {
      public MapperV1( Mapper succesor, Converter converter ) : base(succesor, converter)
      {}

      public override bool CanHandle(Context ctx)
      {
         return ( ctx.ParserData != null ) ? ctx.ParserData.Version == "V1.0" : false;
      }

      protected override Context MapToData(Context ctx)
      {
         try
         {
            var mapData = new DataV1()
            {
               MapSize = ParseInt( ctx.ParserData.Data["Data"]["MapSize"] ),
               AppleManXStartPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManXStartPosition"] ),
               AppleManYStartPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManYStartPosition"] ),
               AppleManSize = ParseDouble( ctx.ParserData.Data["Data"]["AppleManSize"] ),
               AppleManMinimalPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManMinimalPosition"] ),
               AppleManMaximalPosition = ParseDouble( ctx.ParserData.Data["Data"]["AppleManMaximalPosition"] ),
               AppleManMinimalSize = ParseDouble( ctx.ParserData.Data["Data"]["AppleManMinimalSize"] )
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
