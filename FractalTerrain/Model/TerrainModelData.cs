﻿/// <summary>Definition of the class TerrainModelData.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.Model
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
