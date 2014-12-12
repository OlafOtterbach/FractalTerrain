/// <summary>Definition of the class Converter.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Persistence
{
   public abstract class Converter
   {
      public abstract Context Convert(Context ctx);
   }
}
