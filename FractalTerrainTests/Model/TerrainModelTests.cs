/// <summary>Definition of the class TerrainModelTests.</summary>
/// <author>Olaf Otterbach</author>

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractalTerrain;
using System.Collections.Generic;


namespace FractalTerrainTests
{
   [TestClass]
    public class TerrainModelTests
    {
       [TestMethod]
       public void ConstructorTest()
       {
          // Arrange
          var model = new TerrainModel();

          // Test
          Assert.IsNotNull(model);
       }


       [TestMethod]
       public void SizeTest_Input5_Expected5()
       {
          // Arrange
          var model = new TerrainModel();

          // Test
          model.Size = 5;

          // Check
          Assert.AreEqual(5, model.Size);
       }


       [TestMethod]
       public void SizeTest_Input11_Expected9()
       {
          // Arrange
          var model = new TerrainModel();

          // Test
          model.Size = 11;

          // Check
          Assert.AreEqual(9, model.Size);
       }


       [TestMethod]
       public void SizeTest_Input33_Expected33()
       {
          // Arrange
          var model = new TerrainModel();

          // Test
          model.Size = 33;

          // Check
          Assert.AreEqual(33, model.Size);
       }


       [TestMethod]
       public void CreateTest()
       {
          // Arrange
          var model = new TerrainModel();

          // Test
          model.Create(11);

          // Check
          Assert.AreEqual(9, model.Size);
       }


       [TestMethod]
       public void Nextest_Size5_TwoResults()
       {
          // Arrange
          var model = new TerrainModel();

          // Test
          model.Create(5);
          var data1 = model.Next();
          var data2 = model.Next();
          var can = model.CanGetNext();

          // Check
          Assert.AreEqual(data1.Size, 3);
          Assert.AreEqual(data2.Size, 5);
          Assert.IsFalse(can);
       }


       [TestMethod]
       public void Nextest_Size5_AllDataFilled()
       {
          // Arrange
          var model = new TerrainModel();
          model.RandomFunction = () => 4.0;
          model.HeightFactorFunction = (int s, int i) => 1.0;

          // Test
          model.Create(5);
          var data = model.Next();
          data = model.Next();
          
          // Check
          var list = new List<double>();
          var size = data.Size * data.Size;
          for( var i = 0; i < size; i++ )
          {
             list.Add( data.Terrain[i%data.Size, i/data.Size] );
          }
          Assert.AreEqual(data.Size, 5);
          Assert.IsTrue(list.All(x => x > 0.0));
       }


       [TestMethod]
       public void Nextest_Size33_FilledResult()
       {
          // Arrange
          var model = new TerrainModel();

          // Test
          model.Create(33);
          var data = model.Next();
          while( model.CanGetNext() ) data = model.Next();

          // Check
          Assert.AreEqual(data.Size, 33);
       }
   }
}
