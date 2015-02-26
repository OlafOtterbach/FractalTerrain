/// <summary>Definition of the class PersistMapperV1Tests.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class ModelToDataMapperTests
   {
      [TestMethod]
      public void Map_ModelAndSettingsAreNull_StandardValuesMappes()
      {
         var mapper = new ModelToDataMapper();
         
         var data = mapper.Map(null, null);

         Assert.IsNotNull(data);
      }

      [TestMethod]
      public void Map_ModelAndSettings_CorrectMappedValues()
      {
         var pair = PersitenceTestsLib.GetModelAndSettings();
         var mapper = new ModelToDataMapper();

         var data = mapper.Map(pair.Item1, pair.Item2);

         Assert.AreNotEqual(data, null);
         Assert.AreEqual(data.MapSize, 9);
         Assert.AreEqual(data.AppleManMinimalPosition, -5.0);
         Assert.AreEqual(data.AppleManMaximalPosition, 5.0);
         Assert.AreEqual(data.AppleManMinimalSize, 0.1);
         Assert.AreEqual(data.AppleManSize, 4.0 );
         Assert.AreEqual(data.AppleManXStartPosition, -2.0 );
         Assert.AreEqual(data.AppleManYStartPosition, -2.0);
         Assert.AreEqual(data.HoricontalRatio, 0.5);
         Assert.AreEqual(data.VerticalRatio, 0.6);
         Assert.IsNotNull(data.CameraBottomLeft);
         Assert.IsNotNull(data.CameraBottomRight);
         Assert.IsNotNull(data.CameraTopLeft);
         Assert.IsNotNull(data.CameraTopRight);
         Assert.IsNotNull(data.CameraSetting);
         Assert.AreEqual(data.CameraBottomLeft.AngleAxisEz, 0.0);
         Assert.AreEqual(data.CameraBottomLeft.AngleAxisEy, 1.0);
         Assert.AreEqual(data.CameraBottomLeft.Distance, 2.0);
         Assert.AreEqual(data.CameraBottomRight.AngleAxisEz, 3.0);
         Assert.AreEqual(data.CameraBottomRight.AngleAxisEy, 4.0);
         Assert.AreEqual(data.CameraBottomRight.Distance, 5.0);
         Assert.AreEqual(data.CameraTopLeft.AngleAxisEz, 6.0);
         Assert.AreEqual(data.CameraTopLeft.AngleAxisEy, 7.0);
         Assert.AreEqual(data.CameraTopLeft.Distance, 8.0);
         Assert.AreEqual(data.CameraTopRight.AngleAxisEz, 9.0);
         Assert.AreEqual(data.CameraTopRight.AngleAxisEy, 10.0);
         Assert.AreEqual(data.CameraTopRight.Distance, 11.0);
         Assert.AreEqual(data.CameraSetting.AngleAxisEz, 12.0);
         Assert.AreEqual(data.CameraSetting.AngleAxisEy, 13.0);
         Assert.AreEqual(data.CameraSetting.Distance, 14.0);
      }
   }
}
