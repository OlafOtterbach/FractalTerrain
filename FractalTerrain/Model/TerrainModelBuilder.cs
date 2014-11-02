/// <summary>Definition of the class TerrainModelBuilder.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Model
{
   public class TerrainModelBuilder
   {
      public TerrainModelBuilder()
      {
         m_terrainGenerator = new TerrainModelDataGenerator();
         m_appleManGenerator = new AppleManDataGenerator();
      }


      public TerrainModel Create(int mapSize, double appleManXPos, double appleManYPos, double appleManSize )
      {
         var terrainModel = new TerrainModel( m_terrainGenerator, m_appleManGenerator )
                                {
                                   MapSize = mapSize,
                                   AppleManMinimalPosition = -2.0,
                                   AppleManMaximalPosition =  2.0,
                                   AppleManSize = appleManSize,
                                   AppleManXStartPosition = appleManXPos,
                                   AppleManYStartPosition = appleManYPos
                                };
         terrainModel.Update();
         return terrainModel;
      }

      private TerrainModelDataGenerator m_terrainGenerator;

      private AppleManDataGenerator m_appleManGenerator;
   }
}
