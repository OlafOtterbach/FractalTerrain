/// <summary>Definition of the class PersistWriterTests.</summary>
/// <author>Olaf Otterbach</author>

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractalTerrain;
using System.Collections.Generic;
using FractalTerrain.Model;
using FractalTerrain.Persistence;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class PersistWriterTests
   {
      [TestMethod]
      public void SerializeTest()
      {
         var data = GetData();
         var writer = new Writer();

//         var text = writer.Serialize(data);
      }

      private DataV1 GetData()
      {
         var data = new DataV1()
         {
            MapSize = 9,
            AppleManMinimalPosition = -3.0,
            AppleManMaximalPosition = 3.0,
            AppleManMinimalSize = 0.1,
            AppleManSize = 1.0,
            AppleManXStartPosition = -2.0,
            AppleManYStartPosition = 1.5,
         };
         return data;
      }
   }
}
