/// <summary>Definition of the class AppleManData.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.Model
{
   public class AppleManData
   {
      public AppleManData(double[,] map, int size)
      {
         Map = map;
         Size = size;
      }

      public double[,] Map { get; private set; }

      public int Size{ get; private set; }
   }
}
