/// <summary>Definition of the interface IAppleManDataGenerator.</summary>
/// <author>Olaf Otterbach</author>


namespace FractalTerrain
{
   public interface IAppleManDataGenerator
   {
      AppleManData Create(int mapSize, double appleManXPos, double appleManYPos, double appleManSize);
   }
}
