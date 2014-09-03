/// <summary>Definition of the class ViewCameraTest.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Media3D;

namespace FractalTerrainTests
{
   [TestClass]
   public class ViewCameraTest
   {
/*
      [TestMethod]
      public void ConstructorTest()
      {
         var camera = new ViewCamera();

         var offset = camera.Offset.Offset();
         var target = camera.Target;

         const double epsilon = 0.0001;
         Assert.IsTrue(target.IsEqual(new Point3D(0.0, 0.0, 0.0), epsilon));
         Assert.IsTrue(offset.IsEqual(new Point3D(100.0, 100.0, 100.0), epsilon));
      }

      [TestMethod]
      public void SetCameraTest()
      {
         var camera = new ViewCamera();

         camera.SetCamera(new Point3D(0, -100, 0), new Point3D(0,-50,0));

         const double epsilon = 0.0001;
         Assert.IsTrue(camera.Offset.Offset().IsEqual(new Point3D(0.0, -100.0, 0.0), epsilon));
         Assert.IsTrue(camera.Target.IsEqual(new Point3D(0.0, -50.0, 0.0), epsilon));
         Assert.IsTrue(camera.Offset.Ey().IsEqual(new Vector3D(0.0, 1.0, 0.0), epsilon));
         Assert.IsTrue(camera.Offset.Ez().IsEqual(new Vector3D(0.0, 0.0, 1.0), epsilon));
      }


      [TestMethod]
      public void OrbitXYTest()
      {
         var camera = new ViewCamera();
         camera.SetCamera(new Point3D(0, -100, 0), new Point3D(0, -50, 0));

         camera.OrbitXY(90);

         const double epsilon = 0.0001;
         Assert.IsTrue(camera.Offset.Offset().IsEqual(new Point3D(50.0, -50.0, 0.0), epsilon));
         Assert.IsTrue(camera.Target.IsEqual(new Point3D(0.0, -50.0, 0.0), epsilon));
         Assert.IsTrue(camera.Offset.Ey().IsEqual(new Vector3D(-1.0, 0.0, 0.0), epsilon));
         Assert.IsTrue(camera.Offset.Ez().IsEqual(new Vector3D(0.0, 0.0, 1.0), epsilon));
      }


      [TestMethod]
      public void OrbitYZTest()
      {
/*
         var camera = new ViewCamera();
         camera.SetCamera(new Point3D(0, -100, 0), new Point3D(0, -50, 0));

         camera.OrbitYZ(-90);

         const double epsilon = 0.0001;
         Assert.IsTrue(camera.Offset.Offset().IsEqual(new Point3D(0.0, -50.0, 50.0), epsilon));
         Assert.IsTrue(camera.Target.IsEqual(new Point3D(0.0, -50.0, 0.0), epsilon));
         Assert.IsTrue(camera.Offset.Ey().IsEqual(new Vector3D(0.0, 0.0, -1.0), epsilon));
         Assert.IsTrue(camera.Offset.Ez().IsEqual(new Vector3D(0.0, 1.0, 0.0), epsilon));
      }
 */
   }
}
