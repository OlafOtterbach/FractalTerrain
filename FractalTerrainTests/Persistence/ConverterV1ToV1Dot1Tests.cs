/// <summary>Definition of the class ConverterV1ToV1Dot1Tests.</summary>
/// <author>Olaf Otterbach</author>
/// <Date>2015<</Date>

using FractalTerrain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class ConverterV1ToV1Dot1Tests
   {
      [TestMethod]
      public void ConvertTest_Null_EmptyDataV1Dot1()
      {
         var converter = new ConverterV1ToV1Dot1();

         var ctx = converter.Convert(null);

         Assert.IsNotNull(ctx);
         Assert.IsNotNull(ctx.Data as DataV1Dot1);
      }

      [TestMethod]
      public void ConvertTest_DataV1_DataV1Dot1()
      {
         var converter = new ConverterV1ToV1Dot1();
         var ctx = new Context();
         ctx.Data = new DataV1
         {
            MapSize = 1,
            AppleManXStartPosition = 0.1,
            AppleManYStartPosition = 0.2,
            AppleManSize = 2.0,
            AppleManMinimalPosition = 0.0,
            AppleManMaximalPosition = 1.0,
            AppleManMinimalSize = 0.05
         };

         ctx = converter.Convert(ctx);

         var data11 = ctx.Data as DataV1Dot1;
         Assert.IsNotNull(data11);
         Assert.AreEqual(data11.MapSize,1);
         Assert.AreEqual(data11.AppleManXStartPosition,0.1);
         Assert.AreEqual(data11.AppleManYStartPosition,0.2);
         Assert.AreEqual(data11.AppleManSize,2.0);
         Assert.AreEqual(data11.AppleManMinimalPosition, 0.0);
         Assert.AreEqual(data11.AppleManMaximalPosition, 1.0);
         Assert.AreEqual(data11.AppleManMinimalSize, 0.05);

         Assert.AreEqual(data11.HoricontalRatio, 1.0);
         Assert.AreEqual(data11.VerticalRatio, 1.0);

         Assert.IsNotNull(data11.CameraTopLeft);
         Assert.IsNotNull(data11.CameraTopRight);
         Assert.IsNotNull(data11.CameraBottomLeft);
         Assert.IsNotNull(data11.CameraBottomRight);
         Assert.IsNotNull(data11.CameraSetting);

         Assert.AreEqual(data11.CameraBottomLeft.AngleAxisEz, 45);
         Assert.AreEqual(data11.CameraBottomLeft.AngleAxisEy, 25);
         Assert.AreEqual(data11.CameraBottomLeft.Distance, 150);
      }

      [TestMethod]
      public void ConvertTest_DataV1Dot1_NothingCoverted()
      {
         var converter = new ConverterV1ToV1Dot1();
         var ctx = new Context();
         ctx.Data = new DataV1Dot1
         {
            MapSize = 1,
            AppleManXStartPosition = 0.1,
            AppleManYStartPosition = 0.2,
            AppleManSize = 2.0,
            AppleManMinimalPosition = 0.0,
            AppleManMaximalPosition = 1.0,
            AppleManMinimalSize = 0.05,
            HoricontalRatio = 0.5,
            VerticalRatio = 0.5
         };

         ctx = converter.Convert(ctx);

         var data11 = ctx.Data as DataV1Dot1;
         Assert.IsNotNull(data11);
         Assert.AreEqual(data11.MapSize, 1);
         Assert.AreEqual(data11.AppleManXStartPosition, 0.1);
         Assert.AreEqual(data11.AppleManYStartPosition, 0.2);
         Assert.AreEqual(data11.AppleManSize, 2.0);
         Assert.AreEqual(data11.AppleManMinimalPosition, 0.0);
         Assert.AreEqual(data11.AppleManMaximalPosition, 1.0);
         Assert.AreEqual(data11.AppleManMinimalSize, 0.05);
         Assert.AreEqual(data11.HoricontalRatio, 0.5);
         Assert.AreEqual(data11.VerticalRatio, 0.5);
      }
   }
}