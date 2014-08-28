/// <summary>
/// Definition of the class TerrainView3DTest.
/// </summary>
/// <author>
/// Olaf Otterbach
/// </author>

using System.Windows;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractalTerrain;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace FractalTerrainTests
{
   [TestClass]
   public class TerrainView3DTest
   {
      [TestMethod]
      public void DrawLineTest()
      {
         var canvas = new Canvas2DMock();
         canvas.Width = 100.0;
         canvas.Height = 100.0;
         var view = new TerrainView3D(canvas);

         view.DrawLine(new Point3D(-50.0, 0.0, 0.0), new Point3D(50.0, 0.0, 0.0));

         Assert.IsTrue(canvas.Start.X >= 0.0);
         Assert.IsTrue(canvas.Start.X <= 99.0);
         Assert.IsTrue(canvas.Start.Y >= 0.0);
         Assert.IsTrue(canvas.Start.Y <= 99.0);
         Assert.IsTrue(canvas.End.X >= 0.0);
         Assert.IsTrue(canvas.End.X <= 99.0);
         Assert.IsTrue(canvas.End.Y >= 0.0);
         Assert.IsTrue(canvas.End.Y <= 99.0);
         Assert.IsTrue(canvas.Start.X < canvas.End.X);
         Assert.AreEqual(canvas.Start.Y, 50.0, 0.0001);
         Assert.AreEqual(canvas.End.Y, 50.0, 0.0001);
      }

      [TestMethod]
      public void ClippAtLeft_LineCrosses_ClippedLine()
      {
         var start = new Point(-9, -9);
         var end = new Point(10, 10);
         var left = 1.0;

         var exist = TerrainView3D.ClippAtLeft(left, ref start, ref end);

         Assert.IsTrue(exist);
         Assert.AreEqual(start.X, 1.0, 0.0001);
         Assert.AreEqual(start.Y, 1.0, 0.0001);
         Assert.AreEqual(end.X, 10.0, 0.0001);
         Assert.AreEqual(end.Y, 10.0, 0.0001);
      }

      [TestMethod]
      public void ClippAtLeft_LineCrossesEndXLessStarX_ClippedLine()
      {
         var start = new Point(10, 10);
         var end = new Point(-9, -9);
         var left = 1.0;

         var exist = TerrainView3D.ClippAtLeft(left, ref start, ref end);

         Assert.IsTrue(exist);
         Assert.AreEqual(start.X, 1.0, 0.0001);
         Assert.AreEqual(start.Y, 1.0, 0.0001);
         Assert.AreEqual(end.X, 10.0, 0.0001);
         Assert.AreEqual(end.Y, 10.0, 0.0001);
      }

      [TestMethod]
      public void ClippAtLeft_LineOnLeftSide_LineClippedAway()
      {
         var start = new Point(-9, -9);
         var end = new Point(-10, -10);
         var left = 1.0;

         var exist = TerrainView3D.ClippAtLeft(left, ref start, ref end);

         Assert.IsFalse(exist);
      }

      [TestMethod]
      public void ClippAtLeft_LineOnRightSide_LineUnchanged()
      {
         var start = new Point(5, 5);
         var end = new Point(10, 10);
         var left = 1.0;

         var exist = TerrainView3D.ClippAtLeft(left, ref start, ref end);

         Assert.IsTrue(exist);
         Assert.AreEqual(start.X, 5.0, 0.0001);
         Assert.AreEqual(start.Y, 5.0, 0.0001);
         Assert.AreEqual(end.X, 10.0, 0.0001);
         Assert.AreEqual(end.Y, 10.0, 0.0001);
      }

      [TestMethod]
      public void ClippAtRight_LineCrosses_ClippedLine()
      {
         var start = new Point(-9, -9);
         var end = new Point(10, 10);
         var right = 1.0;

         var exist = TerrainView3D.ClippAtRight(right, ref start, ref end);

         Assert.IsTrue(exist);
         Assert.AreEqual(start.X, -9.0, 0.0001);
         Assert.AreEqual(start.Y, -9.0, 0.0001);
         Assert.AreEqual(end.X, 1.0, 0.0001);
         Assert.AreEqual(end.Y, 1.0, 0.0001);
      }

      [TestMethod]
      public void ClippAtRight_LineCrossesEndXLessStartX_ClippedLine()
      {
         var start = new Point(10, 10);
         var end = new Point(-9, -9);
         var right = 1.0;

         var exist = TerrainView3D.ClippAtRight(right, ref start, ref end);

         Assert.IsTrue(exist);
         Assert.AreEqual(start.X, -9.0, 0.0001);
         Assert.AreEqual(start.Y, -9.0, 0.0001);
         Assert.AreEqual(end.X, 1.0, 0.0001);
         Assert.AreEqual(end.Y, 1.0, 0.0001);
      }

      [TestMethod]
      public void ClippAtRight_LineOnRightSide_LineClippedAway()
      {
         var start = new Point(-9, -9);
         var end = new Point(-10, -10);
         var right = 1.0;

         var exist = TerrainView3D.ClippAtLeft(right, ref start, ref end);

         Assert.IsFalse(exist);
      }

      [TestMethod]
      public void ClippAtRight_LineOnLeftSide_LineUnchanged()
      {
         var start = new Point(5, 5);
         var end = new Point(10, 10);
         var left = 1.0;

         var exist = TerrainView3D.ClippAtLeft(left, ref start, ref end);

         Assert.IsTrue(exist);
         Assert.AreEqual(start.X, 5.0, 0.0001);
         Assert.AreEqual(start.Y, 5.0, 0.0001);
         Assert.AreEqual(end.X, 10.0, 0.0001);
         Assert.AreEqual(end.Y, 10.0, 0.0001);
      }
   }
}
