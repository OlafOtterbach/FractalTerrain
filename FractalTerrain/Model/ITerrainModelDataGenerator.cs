/// <summary>Definition of the interface ITerrainModelDataGenerator.</summary>
/// <author>Olaf Otterbach</author>

using System;

namespace FractalTerrain
{
   public interface ITerrainModelDataGenerator
   {
      Func<int, int, double[,], double> RandomFunction { get; set; }
      Func<int, int, double> HeightFactorFunction { get; set; }
      void Update(TerrainModelData terrainModel, AppleManData appleManData);
      TerrainModelData Create(AppleManData appleManData);
   }
}
