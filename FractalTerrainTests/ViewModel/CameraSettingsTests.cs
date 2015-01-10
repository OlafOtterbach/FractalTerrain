/// <summary>Definition of the class CameraSettingsTests.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Linq;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class CameraSettingsTests
   {
      [TestMethod]
      public void ToStringTest()
      {
         var settings = new CameraSettings
         {
            AngleAxisEz = 45.0,
            AngleAxisEy = 30.0,
            Distance = 777.0
         };

         var text = settings.ToString();

         Assert.AreEqual( text, "(45,30,777)" );
      }

      [TestMethod]
      public void ToStringTest_OddValues()
      {
         var settings = new CameraSettings
         {
            AngleAxisEz = 45.1,
            AngleAxisEy = 30.2,
            Distance = 777.3
         };

         var text = settings.ToString();

         Assert.AreEqual( text, "(45.1,30.2,777.3)" );
      }

      [TestMethod]
      public void TryParseTest_Double_CorrectResult()
      {
         var text = "(45.1,30.2,777.3)";

         var camSet = CameraSettings.TryParse( text );

         Assert.AreNotEqual( camSet, null );
         Assert.AreEqual( camSet.AngleAxisEz, 45.1 );
         Assert.AreEqual( camSet.AngleAxisEy, 30.2 );
         Assert.AreEqual( camSet.Distance, 777.3 );
      }

      [TestMethod]
      public void TryParseTest_DoubleAndSpace_CorrectResult()
      {
         var text = "( 45.1, 30.2, 777.3 )";

         var camSet = CameraSettings.TryParse( text );

         Assert.AreNotEqual( camSet, null );
         Assert.AreEqual( camSet.AngleAxisEz, 45.1 );
         Assert.AreEqual( camSet.AngleAxisEy, 30.2 );
         Assert.AreEqual( camSet.Distance, 777.3 );
      }

      [TestMethod]
      public void TryParseTest_OneWrongDouble_Null()
      {
         var text = "(45.1,.,777.3)";

         var camSet = CameraSettings.TryParse( text );

         Assert.AreEqual( camSet, null );
      }

      [TestMethod]
      public void TryParseTest_Integer_CorrectResult()
      {
         var text = "(45,30,777)";

         var camSet = CameraSettings.TryParse( text );

         Assert.AreNotEqual( camSet, null );
         Assert.AreEqual( camSet.AngleAxisEz, 45 );
         Assert.AreEqual( camSet.AngleAxisEy, 30 );
         Assert.AreEqual( camSet.Distance, 777 );
      }


      [TestMethod]
      public void TryParseTest_Wrong_Null()
      {
         var text = "HalloHallo";

         var camSet = CameraSettings.TryParse( text );

         Assert.AreEqual( camSet, null );
      }

      [TestMethod]
      public void Test()
      {
         var text = "(134.5,123,.45)";
         var pattern = @"^(?:\()(?<X>(\d)*(.?)(\d)+)(?:,)(?<Y>(\d)*(.?)(\d)+)(?:,)(?<Z>(\d)*(.?)(\d)+)(?:\))";
         var matches = Regex.Matches( text, pattern, RegexOptions.Singleline );
         var count = matches.Count;
         foreach ( var match in matches )
         {
            var x = double.Parse(( (Match)match ).Groups["X"].Value);
            var y = ( (Match)match ).Groups["Y"].Value;
            var z = ( (Match)match ).Groups["Z"].Value;
         }
      }
   }
}