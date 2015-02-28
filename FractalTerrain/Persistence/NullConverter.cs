/// <summary>Definition of the class NullConverter.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

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
