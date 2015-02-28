/// <summary>Definition of the interface ICanvas2D.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System.Windows;

namespace FractalTerrain.View
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
