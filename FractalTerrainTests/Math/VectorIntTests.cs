/// <summary>Definition of the class VectorIntTests.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain;
using NUnit.Core;
using NUnit.Framework;

namespace FractalTerrainTests
{
   [TestFixture]
   public class VectorIntTests
   {
      [TestCase]
      public void VectorIntTestsConstructorTest()
      {
         var vec = new VectorInt(1, 2);
         Assert.AreEqual(1, vec.X);
         Assert.AreEqual(2, vec.Y);
      }


      [TestCase]
      public void EqualTest_TwoEqualVectors_AreEqual()
      {
         var vec1 = new VectorInt(1, 2);
         var vec2 = new VectorInt(1, 2);

         Assert.IsTrue(vec1 == vec2);
         Assert.IsTrue(vec1.Equals(vec2));
         var obj1 = vec1 as object;
         var obj2 = vec2 as object;
         Assert.IsTrue(obj1.Equals(obj2));
         Assert.IsFalse(vec1 != vec2);
      }

      [Test]
      public void EqualTest_TwoDifferentVectors_AreNotEqual()
      {
         var vec1 = new VectorInt(1, 2);
         var vec2 = new VectorInt(5, 6);

         Assert.IsTrue(vec1 != vec2);
         Assert.IsFalse(vec1 == vec2);
         Assert.IsFalse(vec1.Equals(vec2));
         var obj1 = vec1 as object;
         var obj2 = vec2 as object;
         Assert.IsFalse(obj1.Equals(obj2));
      }

      [Test]
      public void EqualTest_XCoordinateIsEqualYCoordinateIsNotEqual_AreNotEqual()
      {
         var vec1 = new VectorInt(1, 2);
         var vec2 = new VectorInt(1, 6);

         Assert.IsTrue(vec1 != vec2);
         Assert.IsFalse(vec1 == vec2);
         Assert.IsFalse(vec1.Equals(vec2));
         var obj1 = vec1 as object;
         var obj2 = vec2 as object;
         Assert.IsFalse(obj1.Equals(obj2));
      }

      [Test]
      public void EqualTest_XCoordinateIsNotEqualYCoordinateIsEqual_AreNotEqual()
      {
         var vec1 = new VectorInt(1, 2);
         var vec2 = new VectorInt(5, 2);

         Assert.IsTrue(vec1 != vec2);
         Assert.IsFalse(vec1 == vec2);
         Assert.IsFalse(vec1.Equals(vec2));
         var obj1 = vec1 as object;
         var obj2 = vec2 as object;
         Assert.IsFalse(obj1.Equals(obj2));
      }

      [Test]
      public void GetHashCode_EqualVectors_EqualHashCode()
      {
         var vec1 = new VectorInt(1, 2);
         var vec2 = new VectorInt(1, 2);

         Assert.AreEqual(vec1, vec2);
      }

      [Test]
      public void GetHashCode_DifferentVectors_DirfferentHashCode()
      {
         var vec1 = new VectorInt(1, 2);
         var vec2 = new VectorInt(5, 6);
         var vec3 = new VectorInt(1, 6);
         var vec4 = new VectorInt(5, 2);

         Assert.AreNotEqual(vec1, vec2);
         Assert.AreNotEqual(vec1, vec3);
         Assert.AreNotEqual(vec1, vec4);
      }

      [Test]
      public void AddTest_TwoVectors_AddedVector()
      {
         var vec1 = new VectorInt(1, 2);
         var vec2 = new VectorInt(5, 6);

         var addedVec = vec1 + vec2;

         var expectedVector = new VectorInt(6, 8);

         Assert.AreEqual(addedVec, expectedVector);
      }
   }
}
