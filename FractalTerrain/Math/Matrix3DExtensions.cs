/// <summary>Definition of the class Matrix3DExtensions.</summary>
/// <author>Olaf Otterbach</author>
/// <start>12.04.2014</start>
/// <state>2015.02.26</state>

using System;
using System.Windows.Media.Media3D;

namespace FractalTerrain
{
   public static class Matrix3DExtensions
   {
      public static Matrix3D CreateTransform(Point3D offset, Vector3D ey, Vector3D ez)
      {
         var ex = Vector3D.CrossProduct(ey, ez);
         return CreateTransform(offset, ex, ey, ez);
      }

      public static Matrix3D CreateTransform(Point3D offset, Vector3D ex, Vector3D ey, Vector3D ez)
      {
         var transf = new Matrix3D(ex.X, ex.Y, ex.Z, 0.0, ey.X, ey.Y, ey.Z, 0.0, ez.X, ez.Y, ez.Z, 0.0, offset.X, offset.Y, offset.Z, 1.0);
         return transf;
      }


      public static Matrix3D CreateTranslation(Point3D offset)
      {
         var ex = new Vector3D(1.0, 0.0, 0.0);
         var ey = new Vector3D(0.0, 1.0, 0.0);
         var ez = new Vector3D(0.0, 0.0, 1.0);
         var transf = new Matrix3D(ex.X, ex.Y, ex.Z, 0.0, ey.X, ey.Y, ey.Z, 0.0, ez.X, ez.Y, ez.Z, 0.0, offset.X, offset.Y, offset.Z, 1.0);
         return transf;
      }


      public static Matrix3D CreateTranslation(Vector3D translation)
      {
         var ex = new Vector3D(1.0, 0.0, 0.0);
         var ey = new Vector3D(0.0, 1.0, 0.0);
         var ez = new Vector3D(0.0, 0.0, 1.0);
         var transf = new Matrix3D(ex.X, ex.Y, ex.Z, 0.0, ey.X, ey.Y, ey.Z, 0.0, ez.X, ez.Y, ez.Z, 0.0, translation.X, translation.Y, translation.Z, 1.0);
         return transf;
      }


      public static Matrix3D Inverse(this Matrix3D source)
      {
         source.Invert();
         return source;
      }


      public static Point3D Offset(this Matrix3D matrix)
      {
         var offset = new Point3D(matrix.OffsetX, matrix.OffsetY, matrix.OffsetZ);
         return offset;
      }


      public static Vector3D Ex(this Matrix3D matrix)
      {
         var ex = new Vector3D(matrix.M11, matrix.M12, matrix.M13 );
         return ex;
      }


      public static Vector3D Ey(this Matrix3D matrix)
      {
         var ey = new Vector3D(matrix.M21, matrix.M22, matrix.M23);
         return ey;
      }


      public static Vector3D Ez(this Matrix3D matrix)
      {
         var ez = new Vector3D(matrix.M31, matrix.M32, matrix.M33);
         return ez;
      }


      public static Matrix3D CreateRotationXY( double angle )
      {
         var ex = new Vector3D(  Math.Cos(angle), Math.Sin(angle), 0.0);
         var ey = new Vector3D( -Math.Sin(angle), Math.Cos(angle), 0.0 );
         var ez = new Vector3D( 0.0, 0.0, 1.0 );
         var rotationXY = CreateTransform(new Point3D(0.0,0.0,0.0), ex, ey, ez);
         return rotationXY;
      }


      public static Matrix3D CreateRotationYZ( double angle )
      {
         var ex = new Vector3D( 1.0, 0.0, 0.0 );
         var ey = new Vector3D( 0.0,  Math.Cos(angle), Math.Sin(angle) );
         var ez = new Vector3D( 0.0, -Math.Sin(angle), Math.Cos(angle) );
         var rotationYZ = CreateTransform(new Point3D(0.0, 0.0, 0.0), ex, ey, ez);
         return rotationYZ;
      }
   }
}
