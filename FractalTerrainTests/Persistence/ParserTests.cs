/// <summary>Definition of the class HeaderParserTests.</summary>
/// <author>Olaf Otterbach</author>

using System.Linq;
using FractalTerrain.Model;
using FractalTerrain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class ParserTests
   {
      [TestMethod]
      public void ParserTestValidText()
      {
         var text = GetValidText();
         var parser = new Parser();
         var ctx = new Context() { Text = text };

         var res = parser.Parse(ctx);

         Assert.IsTrue(res.Rating.AllSatisfied);
         Assert.AreEqual(res.ParserData.FileType, "FractalTerrain");
         Assert.AreEqual(res.ParserData.Version, "V1.0");
         Assert.AreEqual( res.ParserData.Data.Keys.Count(), 2 );
         Assert.AreEqual( res.ParserData.Data.Keys.ToList()[0], "Header" );
         Assert.AreEqual( res.ParserData.Data.Keys.ToList()[1], "ParserData" );
         Assert.AreEqual( res.ParserData.Data.Values.Count, 2 );
         var headerEntries = res.ParserData.Data.Values.ToList()[0];
         var dataEntries = res.ParserData.Data.Values.ToList()[1];
         Assert.AreEqual(headerEntries.Count, 2);
         Assert.AreEqual(dataEntries.Count, 7);
         Assert.AreEqual(headerEntries["FileType"], "FractalTerrain");
         Assert.AreEqual(headerEntries["Version"], "V1.0");
         Assert.AreEqual(dataEntries["MapSize"],"9");
         Assert.AreEqual(dataEntries["AppleManMinimalPosition"], "-3");
         Assert.AreEqual(dataEntries["AppleManMaximalPosition"], "3");
         Assert.AreEqual(dataEntries["AppleManMinimalSize"],"0.1");
         Assert.AreEqual(dataEntries["AppleManSize"], "1");
         Assert.AreEqual(dataEntries["AppleManXStartPosition"], "-2");
         Assert.AreEqual(dataEntries["AppleManYStartPosition"], "1.5");
      }

      [TestMethod]
      public void ParserTestInvalidDataText()
      {
         var text = GetInvalidDataText();
         var parser = new Parser();
         var ctx = new Context() { Text = text };

         var res = parser.Parse( ctx );

         Assert.IsTrue( res.Rating.AllSatisfied );
      }

      private string GetValidText()
      {
         return
@"[Header]
FileType FractalTerrain
Version V1.0

[Data]
MapSize 9
AppleManMinimalPosition -3
AppleManMaximalPosition 3
AppleManMinimalSize 0.1
AppleManSize 1
AppleManXStartPosition -2
AppleManYStartPosition 1.5
";
      }

      private string GetInvalidDataText()
      {
         return
@"[Header]
FileType FractalTerrain
Version V1.0

[Data]
MapSize 9
AppleManMinimalPosition
AppleManMaximalPosition 3
AppleManMinimalSize 0.1
AppleManSize 1
AppleManXStartPosition -2
AppleManYStartPosition 1.5
";
      }
   }
}