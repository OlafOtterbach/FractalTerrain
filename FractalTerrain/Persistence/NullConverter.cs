/// <summary>Definition of the class NullConverter.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Persistence
{
   public class NullConverter : Converter
   {
      public override Context Convert(Context ctx)
      {
         return ctx;
      }
   }
}
