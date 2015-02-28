/// <summary>Definition of the class WriterTests.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.Persistence;
using FractalTerrain.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class WriterTests
   {
      [TestMethod]
      public void WriteTest()
      {
         var pair = PersitenceTestsLib.GetModelAndSettings();
         var writer = new Writer();

         var text = writer.Write(pair.Item1, pair.Item2);

         Assert.IsTrue(CheckPattern(@"^\[Header\]", text));
         Assert.IsTrue(CheckPattern(@"^MapSize(\s)+9", text));
         Assert.IsTrue(CheckPattern(@"^AppleManMinimalPosition(\s)+-5", text));
         Assert.IsTrue(CheckPattern(@"^AppleManMaximalPosition(\s)+5", text));
         Assert.IsTrue(CheckPattern(@"^AppleManMinimalSize(\s)+0\.1", text));
         Assert.IsTrue(CheckPattern(@"^AppleManSize(\s)+4", text));
         Assert.IsTrue(CheckPattern(@"^AppleManXStartPosition(\s)+-2", text));
         Assert.IsTrue(CheckPattern(@"^AppleManYStartPosition(\s)+-2", text));
         Assert.IsTrue(CheckPattern(@"^\[Settings\]", text));
         Assert.IsTrue(CheckPattern(@"^HoricontalRatio(\s)+0\.5", text));
         Assert.IsTrue(CheckPattern(@"^VerticalRatio(\s)+0\.6", text));
         Assert.IsTrue(CheckPattern(@"^CameraTopLeft(\s)+\(6,7,8\)", text));
         Assert.IsTrue(CheckPattern(@"^CameraTopRight(\s)+\(9,10,11\)", text));
         Assert.IsTrue(CheckPattern(@"^CameraBottomLeft(\s)+\(0,1,2\)", text));
         Assert.IsTrue(CheckPattern(@"^CameraBottomRight(\s)+\(3,4,5\)", text));
         Assert.IsTrue(CheckPattern(@"^CameraSetting(\s)+\(12,13,14\)", text));
      }

      private bool CheckPattern(string pattern, string text)
      {
          var matches = Regex.Matches(text, pattern, RegexOptions.Multiline);
          var count = matches.Count;
          return (count == 1);
      }

      private DataV1Dot1 GetData()
      {
         var data = new DataV1Dot1()
         {
            MapSize = 9,
            AppleManMinimalPosition = -3.0,
            AppleManMaximalPosition = 3.0,
            AppleManMinimalSize = 0.1,
            AppleManSize = 1.0,
            AppleManXStartPosition = -2.0,
            AppleManYStartPosition = 1.5,
            HoricontalRatio = 0.5,
            VerticalRatio = 0.6,
            CameraBottomLeft = new CameraSettings { AngleAxisEz = 0.0, AngleAxisEy = 1.0, Distance = 2.0 },
            CameraBottomRight = new CameraSettings { AngleAxisEz = 3.0, AngleAxisEy = 4.0, Distance = 5.0 },
            CameraTopLeft = new CameraSettings { AngleAxisEz = 6.0, AngleAxisEy = 7.0, Distance = 8.0 },
            CameraTopRight = new CameraSettings { AngleAxisEz = 9.0, AngleAxisEy = 10.0, Distance = 11.0 },
            CameraSetting = new CameraSettings { AngleAxisEz = 12.0, AngleAxisEy = 13.0, Distance = 14.0 },
         };
         return data;
      }
   }
}
