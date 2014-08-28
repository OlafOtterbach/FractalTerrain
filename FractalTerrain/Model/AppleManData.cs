using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalTerrain
{
   public class AppleManData
   {
      public AppleManData(double[,] map, int size)
      {
         Map = map;
         Size = size;
      }

      public double[,] Map { get; private set; }

      public int Size{ get; private set; }
   }
}
