/// <summary>Definition of the class RatingTests.</summary>
/// <author>Olaf Otterbach</author>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractalTerrain;
using FractalTerrain.Model;
using FractalTerrain.Persistence;

namespace FractalTerrainTests.Persistence
{
   [TestClass]
   public class RatingTests
   {
      [TestMethod]
      public void ConstructorTest()
      {
         var rating = new Rating();

         Assert.IsTrue(rating.AllSatisfied);
      }

      [TestMethod]
      public void AllSatisfiedTest()
      {
         var rating = GetRating(0);
         Assert.IsTrue(rating.AllSatisfied);
         var flags = CheckRating(rating);
         Assert.AreEqual(flags, (uint)0);
      }

      [TestMethod]
      public void FlagTest()
      {
         for(uint i = 1; i < 2047; i++)
         {
            var rating = GetRating(i);
            Assert.IsFalse(rating.AllSatisfied);
            Assert.AreEqual(CheckRating(rating), i);
         }
      }

      private Rating GetRating(uint flags)
      {
         var rating = new Rating();
         rating.HasParseError = (flags & 1) > 0;
         rating.HasMappingError = (flags & 2) > 0;
         rating.HasNoModel = (flags & 4) > 0;
         rating.NoModelToValidate = (flags & 8) > 0;
         rating.InvalidMapSize = (flags & 16) > 0;
         rating.InvalidAppleManSize = (flags & 32) > 0;
         rating.HasUnkownVersion = (flags & 64) > 0;
         rating.ResultIsEmpty = (flags & 128) > 0;
         rating.HasCorruptedData = (flags & 256) > 0;
         rating.CanNotSaveFile = (flags & 512) > 0;
         rating.CanNotOpenFile = (flags & 1024) > 0;
         return rating;
      }

      private uint CheckRating(Rating rating)
      {
         uint flags = 0;
         if(rating.HasParseError) flags |= 1;
         if(rating.HasMappingError) flags |= 2;
         if(rating.HasNoModel) flags |= 4;
         if(rating.NoModelToValidate) flags |= 8;
         if(rating.InvalidMapSize) flags |= 16;
         if(rating.InvalidAppleManSize) flags |= 32;
         if(rating.HasUnkownVersion) flags |= 64;
         if(rating.ResultIsEmpty) flags |= 128;
         if(rating.HasCorruptedData) flags |= 256;
         if(rating.CanNotSaveFile) flags |= 512;
         if(rating.CanNotOpenFile) flags |= 1024;
         return flags;
      }
   }
}
