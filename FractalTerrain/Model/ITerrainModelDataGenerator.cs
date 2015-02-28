/// <summary>Definition of the interface ITerrainModelDataGenerator.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System;

namespace FractalTerrain.Model
{
   public interface ITerrainModelDataGenerator
   {
      Func<int, int, double> HeightFactorFunction { get; set; }
      void Update(TerrainModelData terrainModel, AppleManData appleManData);
      TerrainModelData Create(AppleManData appleManData);
   }
}
