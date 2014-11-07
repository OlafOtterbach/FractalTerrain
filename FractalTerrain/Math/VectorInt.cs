/// <summary>Definition of the class VectorInt.</summary>
/// <author>Olaf Otterbach</author>
/// <start>01.07.2014</start>
/// <state>01.07.2014</state>

using System;

namespace FractalTerrain
{
   /// <summary>
   /// Struct for an 2D integer vector.
   /// </summary>
   public struct VectorInt : IEquatable<VectorInt>
   {
      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="x">X coordinate of vector</param>
      /// <param name="y">Y coordinate of vector</param>
      public VectorInt(int x, int y)
         : this()
      {
         X = x;
         Y = y;
      }


      /// <summary>
      /// Accesss to X coordinate of vector.
      /// </summary>
      public int X { get; private set; }


      /// <summary>
      /// Access to y coordinate of vector.
      /// </summary>
      public int Y { get; private set; }


      public override string ToString()
      {
         return "VectorInt(" + X + ", " + Y + ")";
      }

      /// <summary>
      /// Equal operator.
      /// </summary>
      /// <param name="obj">Value to compare</param>
      /// <returns>true=if equal, false= if not equal</returns>
      public override bool Equals(Object obj)
      {
         return (obj is VectorInt) ? Equals( (VectorInt)obj ) : false;
      }


      /// <summary>
      /// Equal operator.
      /// </summary>
      /// <param name="other">Value to compare</param>
      /// <returns>true=if equal, false= if not equal</returns>
      public bool Equals(VectorInt other)
      {
         return (X == other.X) && (Y == other.Y);
      }


      /// <summary>
      /// Equal operator.
      /// </summary>
      /// <param name="first">First value</param>
      /// <param name="second">Second value</param>
      /// <returns>true=if equal, false= if not equal</returns>
      public static bool operator ==(VectorInt first, VectorInt second)
      {
         return first.Equals(second);
      }


      /// <summary>
      /// Unequal operator.
      /// </summary>
      /// <param name="first">First value</param>
      /// <param name="second">Second value</param>
      /// <returns>true=if not equal, false= if equal</returns>
      public static bool operator !=(VectorInt first, VectorInt second)
      {
         return !first.Equals(second);
      }


      public static VectorInt operator -( VectorInt first, VectorInt second )
      {
         var delta = new VectorInt(first.X - second.X, first.Y - second.Y);
         return delta;
      }


      /// <summary>
      /// Add operator.
      /// </summary>
      /// <param name="first">First value</param>
      /// <param name="second">Second value</param>
      /// <returns>Added values</returns>
      public static VectorInt operator + (VectorInt first, VectorInt second)
      {
         return new VectorInt(first.X + second.X, first.Y + second.Y);
      }


      public static VectorInt operator *(VectorInt vec, int scalar)
      {
         return new VectorInt(vec.X * scalar, vec.Y * scalar);
      }


      public static VectorInt operator *(int scalar, VectorInt vec)
      {
         return new VectorInt(vec.X * scalar, vec.Y * scalar);
      }


      public VectorInt Normalized
      {
         get
         {
            var x = ( X == 0 ) ? 0 : ( X > 0 ? 1 : -1 );
            var y = ( Y == 0 ) ? 0 : ( Y > 0 ? 1 : -1 );
            var normalizedVector = new VectorInt(x, y);
            return normalizedVector;
         }
      }


      /// <summary>
      /// Hascode of the VectorInt.
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return X + Y * 397;
      }
   }
}
