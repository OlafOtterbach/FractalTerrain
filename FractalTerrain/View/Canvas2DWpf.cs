/// <summary>Definition of the class Canvas2DWPF.</summary>
/// <author>Olaf Otterbach</author>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FractalTerrain.View
{
   /// <summary>
   /// WPF bitmap canvas to draw 2D geometry.
   /// </summary>
   public class Canvas2DWpf : Canvas2D
   {
      public Canvas2DWpf(Canvas canvas) : base()
      {
         m_canvas = canvas;
         m_image = new Image();
         canvas.Children.Add(m_image);
      }


      public override void Resize()
      {
         int width = (int)m_canvas.ActualWidth;
         int height = (int)m_canvas.ActualHeight;
         m_bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr32, null);
         m_image.Source = m_bitmap;
         Resize(width, height, m_bitmap.BackBufferStride);
      }


      public override void Refresh()
      {
         if( m_bitmap != null )
         {
            m_bitmap.WritePixels(new Int32Rect(0, 0, GetWidth(), GetHeight()), Buffer, m_bitmap.BackBufferStride, 0);
         }
      }


      /// <summary>
      /// Canvas for the bitmap.
      /// </summary>
      Canvas m_canvas;

      /// <summary>
      /// Byte size of the pixel.
      /// </summary>
      private const int M_PixelSize = 4;

      /// <summary>
      /// Image of the bitmap of the canvas.
      /// </summary>
      private Image m_image;

      /// <summary>
      /// Canvas for drawing.
      /// </summary>
      private WriteableBitmap m_bitmap;
   }
}
