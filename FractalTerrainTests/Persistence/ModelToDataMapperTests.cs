/// <summary>Definition of the class PersistMapperV1Tests.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class ModelToDataMapperTests
   {
      //[TestMethod]
      //public void MappingModelToDataTest()
      //{
      //   var model = GetModel(9);
      //   var mapper = new PersistMapper();

      //   var data = mapper.MapModelToPersistenceData(model);

      //   Assert.AreNotEqual(data, null);
      //}


      //[TestMethod]
      //public void PersistenceDataToMapModelTest()
      //{
      //   var model = GetModel(9);
      //   var mapper = new PersistMapper();

      //   var data = mapper.MapModelToPersistenceData(model);
      //   Assert.AreNotEqual(data, null);
      //   var mappedModel = mapper.PersistenceDataToMapModel(data);

      //   Assert.AreNotEqual(mappedModel, null);
      //}


      private TerrainModel GetModel( int mapSize )
      {
         var terrainMap = new double[mapSize, mapSize];
         for( int i = 0; i < mapSize; i++ )
         {
            for( int j = 0; j < mapSize; j++ )
            {
               terrainMap[i, j] = (j+1) * ( i * 10 );
            }
         }
         var terrainData = new TerrainModelData(terrainMap, mapSize);

         var appleMap = new double[mapSize, mapSize];
         for( int i = 0; i < mapSize; i++ )
         {
            for( int j = 0; j < mapSize; j++ )
            {
               appleMap[i, j] = (double)j * (double)( i * 10 ) / (double)( mapSize * 10 );
            }
         }
         var appleData = new AppleManData(appleMap, mapSize);

         var model = new TerrainModel(null, null)
         {
            MapSize = mapSize,
            AppleManMinimalPosition = -5.0,
            AppleManMaximalPosition = 5.0,
            AppleManMinimalSize = 0.1,
            AppleManSize = 4.0,
            AppleManXStartPosition = -2.0,
            AppleManYStartPosition = -2.0,
            TerrainModelData = terrainData,
            AppleManData = appleData
         };
         return model;
      }
   }
}
