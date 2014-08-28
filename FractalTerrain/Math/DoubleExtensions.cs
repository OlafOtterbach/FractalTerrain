/// <summary>Definition of the class DoubleExtensions.</summary>
/// <author>Olaf Otterbach</author>
/// <start>17.04.2014</start>
/// <state>17.04.2014</state>

using System;

namespace FractalTerrain
{
   public static class DoubleExtensions
   {
      public static double DegToRad( this double degAlpha )
      {
         return degAlpha * (Math.PI / 180.0);
      }


      public const double Epsilon = 0.0001;


      public static bool AreEqual(this double first, double second)
      {
         return Math.Abs(first - second) < Epsilon;
      }
   }
}
