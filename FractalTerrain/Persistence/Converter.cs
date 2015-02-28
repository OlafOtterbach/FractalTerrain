/// <summary>Definition of the class Converter.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.Persistence
{
   public abstract class Converter
   {
      public abstract Context Convert(Context ctx);
   }
}
