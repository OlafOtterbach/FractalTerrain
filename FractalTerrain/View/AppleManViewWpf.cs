/// <summary>Definition of the class AppleManViewWpf.</summary>
/// <author>Olaf Otterbach</author>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FractalTerrain
{
   public class AppleManViewWpf
   {
      public AppleManViewWpf(Image image) : base()
      {
//         m_canvas = canvas;
         m_image = image;// new Image();
//         m_image.Stretch = Stretch.Fill;
//         canvas.Children.Add(m_image);
      }

      public TerrainViewModel ViewModel { get; set; }

      public void Render()
      {
         if( m_bitmap != null )
         {
            m_bitmap.WritePixels(new Int32Rect(0, 0, m_width, m_height), m_buffer, m_bitmap.BackBufferStride, 0);
         }
      }

      public void Update()
      {
         if( ViewModel == null )
         {
            return;
         }
         var model = ViewModel.ActualModel;
         if( model != null )
         {
            var appleMan = model.AppleManData;
            m_bitmap = new WriteableBitmap(appleMan.Size, appleMan.Size, 96, 96, PixelFormats.Bgr32, null);
            m_image.Source = m_bitmap;
            Resize(appleMan.Size, appleMan.Size, m_bitmap.BackBufferStride);
            DrawAppleMan();
         }
      }

      private void Resize(int width, int height, int alignWidth)
      {
         m_width = width;
         m_height = height;
         m_alignWidth = alignWidth;
         int memsize = (alignWidth * height) / M_PixelSize;
         m_buffer = new uint[memsize];
      }

      private void DrawAppleMan()
      {
         var ywidth = m_alignWidth / M_PixelSize;
         var appleMan = ViewModel.ActualModel.AppleManData;
         for( int y = 0; y < appleMan.Size; y++ )
         {
            for( int x = 0; x < appleMan.Size; x++ )
            {
               var adr = y * ywidth + x;
               var colorValue = appleMan.Map[x,y];
               var penColor = (uint)( colorValue * 256.0 * 256.0 * 256.0 );
               m_buffer[adr] = penColor;
            }
         }
      }


      private Image m_image;

      private WriteableBitmap m_bitmap;

      private const int M_PixelSize = 4;

      private uint[] m_buffer = null;

      private int m_width;

      private int m_height;

      private int m_alignWidth;
   }
}
