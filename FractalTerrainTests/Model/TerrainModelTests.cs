/// <summary>Definition of the class TerrainModelTests.</summary>
/// <author>Olaf Otterbach</author>

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractalTerrain;
using System.Collections.Generic;
using FractalTerrain.Model;


namespace FractalTerrainTests
{
   [TestClass]
    public class TerrainModelTests
    {
       [TestMethod]
       public void ConstructorTest()
       {
          // Arrange
          var model = new TerrainModel(null,null);

          // Test
          Assert.IsNotNull(model);
       }

       private TerrainModel GetModel() 
       {
          var model = new TerrainModel(null,null)
          {
             AppleManMinimalPosition = -5.0,
             AppleManMaximalPosition = 5.0,
             AppleManSize = 4.0
          };
          return model;
       }

       [TestMethod]
       public void AppleManSizeTestEqualMaximalSize()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -2.0;
          model.AppleManYStartPosition = -2.0;
          model.AppleManSize = 10.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
          Assert.AreEqual(model.AppleManYStartPosition, -5.0);
          Assert.AreEqual(model.AppleManSize, 10.0);
       }

       [TestMethod]
       public void AppleManSizeTestGreaterMaximalSize()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -2.0;
          model.AppleManYStartPosition = -2.0;
          model.AppleManSize = 12.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
          Assert.AreEqual(model.AppleManYStartPosition, -5.0);
          Assert.AreEqual(model.AppleManSize, 10.0);
       }

       [TestMethod]
       public void AppleManSizeTestSmallerMaximalSizeAwayFromLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -2.0;
          model.AppleManYStartPosition = -2.0;
          model.AppleManSize = 2.0;
          Assert.AreEqual(model.AppleManXStartPosition, -1.0);
          Assert.AreEqual(model.AppleManYStartPosition, -1.0);
          Assert.AreEqual(model.AppleManSize, 2.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestClippedRightLessButWiderThanRightLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = 3.0;
          Assert.AreEqual(model.AppleManXStartPosition, 1.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestClippedRightGreaterRightimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = 6.0;
          Assert.AreEqual(model.AppleManXStartPosition, 1.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestSizeEqualRightLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = 1.0;
          Assert.AreEqual(model.AppleManXStartPosition, 1.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestSizeLessRightLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -1.0;
          Assert.AreEqual(model.AppleManXStartPosition, -1.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestEqualLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -5.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestLeftLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -7.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestSizeEqualLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -9.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }

       [TestMethod]
       public void AppleManXStartPositionTestSizeLessLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -12.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestClippedRightLessButWiderThanRightLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = 3.0;
          Assert.AreEqual(model.AppleManXStartPosition, 1.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestClippedRightGreaterRightimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = 6.0;
          Assert.AreEqual(model.AppleManXStartPosition, 1.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestSizeEqualRightLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = 1.0;
          Assert.AreEqual(model.AppleManXStartPosition, 1.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestSizeLessRightLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -1.0;
          Assert.AreEqual(model.AppleManXStartPosition, -1.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestEqualLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -5.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestLeftLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -7.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestSizeEqualLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -9.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }

       [TestMethod]
       public void AppleManYStartPositionTestSizeLessLeftLimit()
       {
          var model = GetModel();
          model.AppleManXStartPosition = -12.0;
          Assert.AreEqual(model.AppleManXStartPosition, -5.0);
       }
    }
}
