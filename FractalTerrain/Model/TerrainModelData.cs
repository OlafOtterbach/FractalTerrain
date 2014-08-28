/// <summary>Definition of the class TerrainModelData.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain
{
   public class TerrainModelData
   {
      public TerrainModelData(double[,] terrain, int size )
      {
         Terrain = terrain;
         Size = size;
      }

      public double[,] Terrain { get; private set; }

      public int Size{ get; private set; }
   }
}
