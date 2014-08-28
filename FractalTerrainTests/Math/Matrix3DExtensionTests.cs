/// <summary>Definition of the class Matrix3DExtensionTests.</summary>
/// <author>Olaf Otterbach</author>

using System.Windows.Media.Media3D;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractalTerrain;
using System.Collections.Generic;


namespace FractalTerrainTests
{
   [TestClass]
   public class Matrix3DExtensionTests
   {
      [TestMethod]
      public void CreateTransformTest()
      {
         var mat = Matrix3DExtensions.CreateTransform(new Point3D(0, 0, 0), new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1));
         var vec = new Vector3D(1, 0, 0);

         var transVec = vec * mat;

         Assert.IsTrue(transVec.IsEqual(new Vector3D(0.0, 1.0, 0.0), 0.0001));
      }


      [TestMethod]
      public void CreateTransform_TranslationOrientationTest()
      {
         var mat = Matrix3DExtensions.CreateTransform(new Point3D(0, 0, 0), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1), new Vector3D(0,1,0) );
         var pnt = new Point3D(1, 2, 3);

         var transPnt = pnt * mat;

         Assert.IsTrue(transPnt.IsEqual(new Point3D(1.0, 3.0, 2.0), 0.0001));
      }

      [TestMethod]
      public void CreateTransform_ProjectionToXY()
      {
         var mat = Matrix3DExtensions.CreateTransform(new Point3D(0, 0, 0), new Vector3D(1, 0, 0), new Vector3D(0, 0, 0), new Vector3D(0, 1, 0));
         var pnt = new Point3D(1, 2, 3);

         var transPnt = pnt * mat;

         Assert.IsTrue(transPnt.IsEqual(new Point3D(1.0, 3.0, 0.0), 0.0001));
      }

      [TestMethod]
      public void CreateRotationXYTest()
      {
         var mat = Matrix3D.Identity;

         var rot = Matrix3DExtensions.CreateRotationXY(90.0.DegToRad());
         var rotmat = mat * rot;

         Assert.IsTrue(rotmat.Ex().IsEqual(new Vector3D(0.0, 1.0, 0.0), 0.0001));
      }

      [TestMethod]
      public void CreateRotationYZTest()
      {
         var mat = Matrix3D.Identity;

         var rot = Matrix3DExtensions.CreateRotationYZ(90.0.DegToRad());
         var rotmat = mat * rot;

         Assert.IsTrue(rotmat.Ey().IsEqual(new Vector3D(0.0, 0.0, 1.0), 0.0001));
      }
   }
}