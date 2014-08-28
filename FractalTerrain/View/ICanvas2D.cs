/// <summary>Definition of the interface ICanvas2D.</summary>
/// <author>Olaf Otterbach</author>
/// <start>11.04.2014</start>
/// <state>11.04.2014</state>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FractalTerrain
{
   /// <summary>
   /// Interface for canvas to draw 2D geometry.
   /// </summary>
   public interface ICanvas2D
   {
      double Width { get; }
      double Height { get; }
      void SetPen(uint color);
      void SetPen(int red, int green, int blue);
      void DrawLine(Point start, Point end);
      void Clear();
      void Resize();
      void Refresh();
   }
}
