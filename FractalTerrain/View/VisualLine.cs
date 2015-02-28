/// <summary>Definition of the struct VisualLine.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System.Windows.Media.Media3D;

namespace FractalTerrain.View
{
   /// <summary>
   /// Struct representing line for visualisation.
   /// </summary>
   public class VisualLine
   {
      public VisualLine( Point3D start, Point3D end)
      {
         Start = start;
         End = end;
         SetPen(255, 255, 255);
      }

      public Point3D Start { get; private set; }

      public Point3D End { get; private set; }
      
      public uint Color { get; private set; }

      public void SetPen(int red, int green, int blue)
      {
         uint ured = (uint)red;
         uint ugreen = (uint)green;
         uint ublue = (uint)blue;
         Color = ublue | (ugreen << 8) | (ured << 16) | (0xFF000000);
      }
   }
}
