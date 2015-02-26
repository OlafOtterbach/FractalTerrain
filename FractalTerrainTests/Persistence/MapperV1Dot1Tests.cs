/// <summary>Definition of the class ParserTests.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class MapperV1Dot1Tests
   {
      [TestMethod]
      public void MapValidTextTest()
      {
         var parser = new Parser();
         var originalText = GetValidText();
         var ctx = new Context() { Text = originalText };
         ctx = parser.Parse( ctx );
         var mapper = new MapperV1Dot1( new MapperUnknown(), null );
         ctx = mapper.Map(ctx);
         
         var data = ctx.Data as DataV1Dot1;
         Assert.AreEqual(data.MapSize, 9);
         Assert.AreEqual(data.AppleManMinimalPosition, -5.0);
         Assert.AreEqual(data.AppleManMaximalPosition,  5.0);
         Assert.AreEqual(data.AppleManMinimalSize, 0.1);
         Assert.AreEqual(data.AppleManSize, 4.0);
         Assert.AreEqual(data.AppleManXStartPosition, -2.0);
         Assert.AreEqual(data.AppleManYStartPosition, -2.0);
         Assert.AreEqual(data.HoricontalRatio, 0.5);
         Assert.AreEqual(data.VerticalRatio, 0.6);
         Assert.AreEqual(data.CameraBottomLeft.AngleAxisEz, 0);
         Assert.AreEqual(data.CameraBottomLeft.AngleAxisEy, 1);
         Assert.AreEqual(data.CameraBottomLeft.Distance, 2);
         Assert.AreEqual(data.CameraBottomRight.AngleAxisEz, 3);
         Assert.AreEqual(data.CameraBottomRight.AngleAxisEy, 4);
         Assert.AreEqual(data.CameraBottomRight.Distance, 5);
         Assert.AreEqual(data.CameraTopLeft.AngleAxisEz, 6);
         Assert.AreEqual(data.CameraTopLeft.AngleAxisEy, 7);
         Assert.AreEqual(data.CameraTopLeft.Distance, 8);
         Assert.AreEqual(data.CameraTopRight.AngleAxisEz, 9);
         Assert.AreEqual(data.CameraTopRight.AngleAxisEy, 10);
         Assert.AreEqual(data.CameraTopRight.Distance, 11);
         Assert.AreEqual(data.CameraSetting.AngleAxisEz, 12);
         Assert.AreEqual(data.CameraSetting.AngleAxisEy, 13);
         Assert.AreEqual(data.CameraSetting.Distance, 14);
      }

      private string GetValidText()
      {
         return
@"[Header]
FileType FractalTerrain
Version V1.1

[Data]
MapSize 9
AppleManMinimalPosition -5
AppleManMaximalPosition 5
AppleManMinimalSize 0.1
AppleManSize 4
AppleManXStartPosition -2
AppleManYStartPosition -2

[Settings]
HoricontalRatio 0.5
VerticalRatio 0.6
CameraTopLeft (6,7,8)
CameraTopRight (9,10,11)
CameraBottomLeft (0,1,2)
CameraBottomRight (3,4,5)
CameraSetting (12,13,14)
";
      }
   }
}