/// <summary>Definition of the class TerrainModelBuilder.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain
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
                                   AppleManXStartPosition = appleManXPos,
                                   AppleManYStartPosition = appleManYPos,
                                   AppleManSize = appleManSize,
                                   MinimalStartPosition = -2.5,
                                   MaximalStartPosition =  1.0
                                };
         terrainModel.Update();
         return terrainModel;
      }

      private TerrainModelDataGenerator m_terrainGenerator;

      private AppleManDataGenerator m_appleManGenerator;
   }
}
