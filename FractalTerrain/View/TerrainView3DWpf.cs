/// <summary>Definition of the class TerrainView3DBehaviour.</summary>
/// <author>Olaf Otterbach</author>
/// <start>20.04.2014</start>
/// <state>20.04.2014</state>

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace FractalTerrain
{
   /// <summary>
   /// View with Wpf events.
   /// </summary>
   public class TerrainView3DWpf : TerrainView3D
   {
      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="canvas">Canvas of the view</param>
      public TerrainView3DWpf(ICanvas2D canvas) : base(canvas)
      {
         // empty
      }


      /// <summary>
      /// Bearbeitet MouseDown-Ereignis.
      /// </summary>
      /// <param name="sender">Objekt, das Ereignis ausgeloest hat</param>
      /// <param name="inputEvent">Ereignis, das ausgeloest wurde</param>
      public void OnMouseDown(object sender, MouseButtonEventArgs inputEvent)
      {
         Point point = inputEvent.GetPosition((UIElement)sender);
         m_mouseX = point.X;
         m_mouseY = point.Y;
      }

      
      /// <summary>
      /// Bearbeitet MouseMove-Ereignis.
      /// </summary>
      /// <param name="t_sender">Objekt, das Ereignis ausgeloest hat</param>
      /// <param name="inputEvent">Ereignis, das ausgeloest wurde</param>
      public void OnMouseMove(object t_sender, MouseEventArgs inputEvent)
      {
         if( (inputEvent.LeftButton == MouseButtonState.Pressed) || (inputEvent.RightButton == MouseButtonState.Pressed) )
         {
            Point point = inputEvent.GetPosition((UIElement)t_sender);
            double deltaX = point.X - m_mouseX;
            double deltaY = point.Y - m_mouseY;
            m_mouseX = point.X;
            m_mouseY = point.Y;
            double dx = 0.0;
            double dy = 0.0;
            if( (inputEvent.LeftButton == MouseButtonState.Pressed) && (inputEvent.RightButton == MouseButtonState.Released))
            {
                  dx = ((double)(-deltaX));
                  dy = ((double)(-deltaY));
                  Camera.OrbitYZ(dy);
                  Camera.OrbitXY(dx);
                  Render();
            }
            if( (inputEvent.RightButton == MouseButtonState.Pressed) && (inputEvent.LeftButton == MouseButtonState.Released))
            {
                  dy = ((double)deltaY) * 10.0;
                  Camera.Zoom(dy);
                  Render();
            }
         }
      }


      /// <summary>
      /// Aktuelle X-Mauskoordinate.
      /// </summary>
      double m_mouseX = 0;

      /// <summary>
      /// Aktuelle Y-Mauskoordinate.
      /// </summary>
      double m_mouseY = 0;
   }
}
