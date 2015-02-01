/// <summary>Definition of the class AppleManViewWpf.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.ViewModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FractalTerrain.View
{
   public class AppleManView
   {
      public AppleManView(Image image) : base()
      {
         m_image = image;
      }

      public TerrainViewModel ViewModel { get; set; }

      public void Render()
      {
         if( m_bitmap != null )
         {
            m_bitmap.WritePixels(new Int32Rect(0, 0, m_width, m_height), m_buffer, m_bitmap.BackBufferStride, 0);
         }
      }

      public void Update( AppleManSettings settings )
      {
         m_bitmap = new WriteableBitmap( settings.MapSize, settings.MapSize, 96, 96, PixelFormats.Bgr32, null );
         m_image.Source = m_bitmap;
         Resize( settings.MapSize, settings.MapSize, m_bitmap.BackBufferStride );
         DrawAppleMan(settings);
      }

      private void DrawAppleMan( AppleManSettings settings )
      {
         var ywidth = m_alignWidth / M_PixelSize;
         Parallel.For( 0, settings.MapSize, y =>
         {
            for ( int x = 0; x < settings.MapSize; x++ )
            {
               var adr = y * ywidth + x;
               var colorValue = settings.Map[x, y];
               var penColor = (uint)( colorValue * 256.0 * 256.0 * 256.0 );
               m_buffer[adr] = penColor;
            }
         } );
      }
      
      private void Resize(int width, int height, int alignWidth)
      {
         m_width = width;
         m_height = height;
         m_alignWidth = alignWidth;
         int memsize = (alignWidth * height) / M_PixelSize;
         m_buffer = new uint[memsize];
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
