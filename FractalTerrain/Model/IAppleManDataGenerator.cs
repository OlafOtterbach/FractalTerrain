/// <summary>Definition of the interface IAppleManDataGenerator.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.Model
{
   public interface IAppleManDataGenerator
   {
      AppleManData Create(int mapSize, double appleManXPos, double appleManYPos, double appleManSize);
   }
}
