/// <summary>Definition of the class Vector3DExtensions.</summary>
/// <author>Olaf Otterbach</author>
/// <start>14.04.2014</start>
/// <state>14.04.2014</state>

using System;
using System.Windows.Media.Media3D;

namespace FractalTerrain
{
   public static class Vector3DExtensions
   {
      public static Vector3D Normalized(this Vector3D vector)
      {
         var normalVector = new Vector3D(vector.X, vector.Y, vector.Z);
         normalVector.Normalize();
         return normalVector;
      }


      public static bool IsCollinear(this Vector3D vecA, Vector3D vecB)
      {
         var normA = vecA.Normalized();
         var normB = vecB.Normalized();
         const double epsilon = 0.00001;
         bool isCollinear = (normA.IsEqual(normB, epsilon)) || (normA.IsEqual(-normB, epsilon));
         return isCollinear;
      }


      public static bool IsEqual(this Vector3D vecA, Vector3D vecB, double epsilon)
      {
         var isEqual =    (Math.Abs(vecA.X - vecB.X) < epsilon)
                       && (Math.Abs(vecA.Y - vecB.Y) < epsilon)
                       && (Math.Abs(vecA.Z - vecB.Z) < epsilon);
         return isEqual;
      }
   }
}
