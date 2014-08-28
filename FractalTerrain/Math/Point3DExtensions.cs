/// <summary>Definition of the class Point3DExtensions.</summary>
/// <author>Olaf Otterbach</author>
/// <start>14.04.2014</start>
/// <state>14.04.2014</state>

using System;
using System.Windows.Media.Media3D;

namespace FractalTerrain
{
   public static class Point3DExtensions
   {
      public static bool IsEqual(this Point3D pointA, Point3D pointB, double epsilon)
      {
         var isEqual =    (Math.Abs(pointA.X - pointB.X) < epsilon)
                       && (Math.Abs(pointA.Y - pointB.Y) < epsilon)
                       && (Math.Abs(pointA.Z - pointB.Z) < epsilon);
         return isEqual;
      }
   }
}
