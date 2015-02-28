/// <summary>Definition of the interface Canvas2DMock.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.View;
using System.Windows;

namespace FractalTerrainTests
{
   public class Canvas2DMock : ICanvas2D
   {
      public Canvas2DMock()
      {
         Width = 100;
         Height = 100;
      }

      public double Width { get; set;}
      public double Height { get; set;}
      public void SetPen(uint color){}
      public void SetPen(int red, int green, int blue) {}
      public void Clean() {}
      public void DrawLine(Point start, Point end)
      {
         Start = start;
         End = end;
      }
      public void Refresh() { }
      public void Resize() { }
      public void Clear() { }

      public Point Start { get; private set; }
      public Point End { get; private set; }
   }
}
