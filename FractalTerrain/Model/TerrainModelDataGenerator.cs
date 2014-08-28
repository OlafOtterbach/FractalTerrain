﻿/// <summary>Definition of the class TerrainModelGenerator.</summary>
/// <author>Olaf Otterbach</author>

using System;

namespace FractalTerrain
{
   public class TerrainModelDataGenerator : ITerrainModelDataGenerator
   {
      public TerrainModelDataGenerator()
      {
         RandomFunction = (x, y, map) => { var rand = map[x, y]; return rand; };
         HeightFactorFunction = (int size, int iteration) => { return Math.Pow(0.55, iteration); };
      }


      public Func<int,int,double[,],double> RandomFunction { get; set; }


      public Func<int, int, double> HeightFactorFunction { get; set; }

      public void Update(TerrainModelData terrainModel, AppleManData appleManData)
      {
         Calculate(terrainModel, appleManData);
      }

      public TerrainModelData Create( AppleManData appleManData )
      {
         var mapSize = appleManData.Size;

         var terrain = new double[mapSize, mapSize];
         var terrainModel = new TerrainModelData(terrain, mapSize);

         Calculate(terrainModel, appleManData);

         return terrainModel;
      }

      private void Calculate(TerrainModelData terrainModel, AppleManData appleManData  )
      {
         var terrain = terrainModel.Terrain;
         var min = 0;
         var max = terrainModel.Size - 1;
         var map = appleManData.Map;
         terrain[min, min] = RandomFunction(min, min, map);
         terrain[max, min] = RandomFunction(max, min, map);
         terrain[min, max] = RandomFunction(min, max, map);
         terrain[max, max] = RandomFunction(max, max, map);

         var step = terrainModel.Size - 1;
         var index = 1;
         const double minimal_calculation_size = 2;
         while( step >= minimal_calculation_size )
         {
            var factor = HeightFactorFunction(terrainModel.Size, index++);
            CalculateIteration(step, factor, terrainModel, appleManData);
            step /= 2;
         }
      }

      private void CalculateIteration(int step, double factor, TerrainModelData terrainModel, AppleManData appleManData)
      {
         var size = terrainModel.Size;
         var mapSize = size / step;
         var map = appleManData.Map;
         for( var y = 0; y < size - 1; y += step )
         {
            for( var x = 0; x < size - 1; x += step )
            {
               CalculationSingleStep(x, y, step, factor, terrainModel, appleManData);
            }
         }
      }

      private void CalculationSingleStep(int x, int y, int step, double factor, TerrainModelData terrainModel, AppleManData appleManData)
      {
         var terrain = terrainModel.Terrain;
         var map = appleManData.Map;
         var xmin = x;
         var ymin = y;
         var xmax = x + step;
         var ymax = y + step;
         var halfstep = step / 2;
         var xmiddle = x + halfstep;
         var ymiddle = y + halfstep;
         terrain[xmiddle, ymiddle] = ( terrain[xmin, ymin] + terrain[xmax, ymin] + terrain[xmax, ymax] + terrain[xmin, ymax] ) / 4.0 + RandomFunction(xmiddle, ymiddle, map) * factor;
         terrain[xmiddle, ymin] = ( terrain[xmin, ymin] + terrain[xmax, ymin] ) / 2.0 + RandomFunction(xmiddle, ymin, map) * factor;
         terrain[xmin, ymiddle] = ( terrain[xmin, ymin] + terrain[xmin, ymax] ) / 2.0 + RandomFunction(xmin, ymiddle, map) * factor;
         terrain[xmax, ymiddle] = ( terrain[xmax, ymin] + terrain[xmax, ymax] ) / 2.0 + RandomFunction(xmax, ymiddle, map) * factor;
         terrain[xmiddle, ymax] = ( terrain[xmin, ymax] + terrain[xmax, ymax] ) / 2.0 + RandomFunction(xmiddle, ymax, map) * factor;
      }
   }
}
