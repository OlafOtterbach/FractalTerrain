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

      public TerrainModel Create( int mapSize, double appleManXPos, double appleManYPos, double appleManSize )
      {
         const double appleManMinimalSize = 0.0005;
         const double appleManMinimalPosition = -2.0;
         const double appleManMaximalPosition = 2.0;
         return Create( mapSize,
                        appleManMinimalSize,
                        appleManMinimalPosition,
                        appleManMaximalPosition,
                        appleManXPos,
                        appleManYPos,
                        appleManSize
                     );
      }

      public TerrainModel Create
      (
         int mapSize,
         double appleManMinimalSize,
         double appleManMinimalPosition,
         double appleManMaximalPosition,
         double appleManXPos, 
         double appleManYPos, 
         double appleManSize
      )
      {
         var terrainModel = new TerrainModel(m_terrainGenerator, m_appleManGenerator)
                                {
                                   MapSize = mapSize,
                                   AppleManMinimalSize = appleManMinimalSize,
                                   AppleManMinimalPosition = appleManMinimalPosition,
                                   AppleManMaximalPosition = appleManMaximalPosition,
                                   AppleManSize = appleManSize,
                                   AppleManXStartPosition = appleManXPos,
                                   AppleManYStartPosition = appleManYPos,
                                };
         terrainModel.Update();
         return terrainModel;
      }

      private TerrainModelDataGenerator m_terrainGenerator;

      private AppleManDataGenerator m_appleManGenerator;
   }
}
