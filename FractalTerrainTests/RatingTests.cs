/// <summary>Definition of the class RatingTests.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain;
using FractalTerrain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class RatingTests
   {
      [TestMethod]
      public void RatingTestUnchangedAllsatisfiedOthersAreFalse()
      {
         var rating = new Rating();
         Assert.IsTrue( rating.AllSatisfied);
         Assert.IsFalse(rating.HasParseError);
         Assert.IsFalse(rating.HasUnkownVersion);
      }
      [TestMethod]
      public void RatingTestHasUnkownVersionIsTrueAllsatisfiedAndOthersAreFalse()
      {
         var rating = new Rating();

         rating.HasUnkownVersion = true;
         
         Assert.IsFalse(rating.AllSatisfied);
         Assert.IsFalse(rating.HasParseError);
         Assert.IsTrue(rating.HasUnkownVersion);
      }
      [TestMethod]
      public void RatingTestHasParseErrorIsTrueAllsatisfiedAndOthersAreFalse()
      {
         var rating = new Rating();

         rating.HasParseError = true;

         Assert.IsFalse(rating.AllSatisfied);
         Assert.IsTrue(rating.HasParseError);
         Assert.IsFalse(rating.HasUnkownVersion);
      }
      [TestMethod]
      public void RatingTestHasParseErrorAndHasUnkownVersionAreTrueAllsatisfiedIsFalse()
      {
         var rating = new Rating();
         rating.HasParseError = true;
         Assert.IsFalse(rating.AllSatisfied);
         Assert.IsTrue(rating.HasParseError);
         Assert.IsFalse(rating.HasUnkownVersion);
      }
   }
}