/// <summary>Definition of the class AppleManDataGenerator.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System.Threading.Tasks;

namespace FractalTerrain.Model
{
   public class AppleManDataGenerator : IAppleManDataGenerator
   {
      public AppleManData Create( int mapSize, double appleManXPos, double appleManYPos, double appleManSize )
      {
         var map = new double[mapSize, mapSize];
         const double limit = 4.0;
         int iterations = 100;
         double xmin = appleManXPos;
         double ymin = appleManYPos;
         double xmax = appleManXPos + appleManSize;
         double ymax = appleManYPos + appleManSize;
         double step = ( xmax - xmin ) / mapSize;
         double maxVal = 0.0;
         Parallel.For(0, mapSize, yw =>
         {
            var ypos = ymin + yw * step;
            var xpos = xmin;
            for( int xw = 0; xw < mapSize; xw++ )
            {
               int loops = 0;
               double z = 0;
               double zi = 0;
               for( int k = 0; k < iterations; k++ )
               {
                  double newz = z * z - zi * zi + xpos;
                  double newzi = 2.0f * z * zi + ypos;
                  z = newz;
                  zi = newzi;
                  double val = z * z + zi * zi;
                  if( val > limit )
                  {
                     loops = k;
                     break;
                  }
               }
               if( loops != iterations )
               {
                  map[xw, yw] = (double)loops / (double)iterations;
                  if( map[xw, yw] > maxVal )
                  {
                     maxVal = map[xw, yw];
                  }
               }
               else
               {
                  map[xw, yw] = 0.0;
               }
               xpos += step;
            }
         });
         if( maxVal > 0.0 )
         {
            Parallel.For(0, mapSize, yw =>
            {
               for( int xw = 0; xw < mapSize; xw++ )
               {
                  map[xw, yw] = map[xw, yw] / maxVal;
               }
            });
         }
         var data = new AppleManData(map, mapSize);
         return data;
      }
   }
}
