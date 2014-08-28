/// <summary>Definition of the class Canvas2D.</summary>
/// <author>Olaf Otterbach</author>
/// <start>11.04.2014</start>
/// <state>01.05.2014</state>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FractalTerrain
{
   /// <summary>
   /// Canvas to draw 2D geometry.
   /// </summary>
   public abstract class Canvas2D : ICanvas2D
   {
      public Canvas2D()
      {
         SetPen(0, 255, 0);
      }


      public double Width { get { return m_width; } }

      public double Height { get { return m_height; } }

      public abstract void Resize();

      public abstract void Refresh();


      public void SetPen(double red, double green, double blue)
      {
         SetPen(red * 255.0, green * 255.0, blue * 255.0);
      }


      public void SetPen( int red, int green, int blue )
      {
         uint ured = (uint)red;
         uint ugreen = (uint)green;
         uint ublue = (uint)blue;
         m_penColor = ublue | (ugreen << 8) | (ublue << 16) | (0xFF000000);
      }

      public void SetPen(uint color)
      {
         m_penColor = color;
      }


      public void DrawLine( Point start, Point end )
      {
         if (ClippAtLeft(0.0, ref start, ref end))
         {
            if (ClippAtRight(m_width - 1.0, ref start, ref end))
            {
               if (ClippAtBottom(0.0, ref start, ref end))
               {
                  if (ClippAtTop(m_height - 1.0, ref start, ref end))
                  {
                     DrawLineHiddenLineMode2((int)start.X, (int)start.Y, (int)end.X, (int)end.Y);
                  }
               }
            }
         }
      }


      public void Clear()
      {
         if (m_buffer != null)
         {
            Array.Copy(m_clearBuffer, m_buffer, m_buffer.Length);
            Array.Copy(m_clearYBuffer, m_ybuffer, m_ybuffer.Length);
         }
      }


      protected uint[] Buffer { get { return m_buffer; } }


      protected void Resize(int width, int height, int alignWidth)
      {
         m_width = width;
         m_height = height;
         m_alignWidth = alignWidth;
         int memsize = (alignWidth * height) / M_PixelSize;
         m_buffer = new uint[memsize];
         m_clearBuffer = new uint[memsize];
         m_ybuffer = new int[alignWidth / M_PixelSize];
         m_clearYBuffer = new int[alignWidth / M_PixelSize];
         int clearSize = m_clearBuffer.Length;
         for (int i = 0; i < clearSize; i++)
         {
            m_clearBuffer[i] = 0x00000000;
         }
         int clearYSize = m_clearYBuffer.Length;
         for (int i = 0; i < clearYSize; i++)
         {
            m_clearYBuffer[i] = int.MinValue;
         }        
         Clear();
      }


      protected int GetWidth()
      {
         return m_width;
      }


      protected int GetHeight()
      {
         return m_height;
      }


      private void DrawLine(int x0, int y0, int x1, int y1)
      {
         var ywidth = m_alignWidth / M_PixelSize;
         int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
         int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
         int err = dx + dy;
         int e2; /* error value e_xy */
         while (true)
         {
            var adresse = y0 * ywidth + x0;
            m_buffer[adresse] = m_penColor;
            if (x0 == x1 && y0 == y1) break;
            e2 = 2 * err;
            if (e2 > dy) { err += dy; x0 += sx; } /* e_xy+e_x > 0 */
            if (e2 < dx) { err += dx; y0 += sy; } /* e_xy+e_y < 0 */
         }
      }


      private void DrawLineHiddenLineMode2(int x0, int y0, int x1, int y1)
      {
         if (y0 > y1)
         {
            var help = y0;
            y0 = y1;
            y1 = help;
            help = x0;
            x0 = x1;
            x1 = help;
         }
         var ywidth = m_alignWidth / M_PixelSize;
         int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
         int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
         int err = dx + dy;
         int e2; /* error value e_xy */
         while (true)
         {
            if (y0 > m_ybuffer[x0])
            {
               var adresse = ( m_height - y0 - 1 ) * ywidth + x0;
               m_buffer[adresse] = m_penColor;
               m_ybuffer[x0] = y0;
            }
            if (x0 == x1 && y0 == y1) break;
            e2 = 2 * err;
            if (e2 > dy) { err += dy; x0 += sx; } /* e_xy+e_x > 0 */
            if (e2 < dx) { err += dx; y0 += sy; } /* e_xy+e_y < 0 */
         }
      }


      /// <summary>
      /// Clips a line at a line at the left of the view field.
      /// </summary>
      /// <param name="left">Left limit of the view field</param>
      /// <param name="start">Start point of the line</param>
      /// <param name="end">End point of the line</param>
      /// <returns>bool  true=line exists, false=line is clipped away</returns>
      private static bool ClippAtLeft(double left, ref Point start, ref Point end)
      {
         if ((start.X < left) && (end.X < left))
         {
            return false;
         }
         else if ((start.X >= left) && (end.X >= left))
         {
            return true;
         }
         else
         {
            if (start.X > end.X)
            {
               var help = start;
               start = end;
               end = help;
            }
            double qx = (left - start.X) / (end.X - start.X);
            double deltay = qx * (end.Y - start.Y);
            start = new Point(left, start.Y + deltay);
         }
         return true;
      }


      /// <summary>
      /// Clips a line at a line at the right of the view field.
      /// </summary>
      /// <param name="right">Right limit of the view field</param>
      /// <param name="start">Start point of the line</param>
      /// <param name="end">End point of the line</param>
      /// <returns>bool  true=line exists, false=line is clipped away</returns>
      private static bool ClippAtRight(double right, ref Point start, ref Point end)
      {
         if ((start.X > right) && (end.X > right))
         {
            return false;
         }
         else if ((start.X <= right) && (end.X <= right))
         {
            return true;
         }
         else
         {
            if (start.X > end.X)
            {
               var help = start;
               start = end;
               end = help;
            }
            double qx = (right - start.X) / (end.X - start.X);
            double deltay = qx * (end.Y - start.Y);
            end = new Point(right, start.Y + deltay);
         }
         return true;
      }


      /// <summary>
      /// Clips a line at a line at the bottom of the view field.
      /// </summary>
      /// <param name="bottom">Bottom limit of the view field</param>
      /// <param name="start">Start point of the line</param>
      /// <param name="end">End point of the line</param>
      /// <returns>bool  true=line exists, false=line is clipped away</returns>
      private static bool ClippAtBottom(double bottom, ref Point start, ref Point end)
      {
         if ((start.Y < bottom) && (end.Y < bottom))
         {
            return false;
         }
         else if ((start.Y >= bottom) && (end.Y >= bottom))
         {
            return true;
         }
         else
         {
            if (start.Y > end.Y)
            {
               var help = start;
               start = end;
               end = help;
            }
            double qy = (bottom - start.Y) / (end.Y - start.Y);
            double deltax = qy * (end.X - start.X);
            start = new Point(start.X + deltax, bottom);
         }
         return true;
      }


      /// <summary>
      /// Clips a line at a line at the top of the view field.
      /// </summary>
      /// <param name="top">Top limit of the view field</param>
      /// <param name="start">Start point of the line</param>
      /// <param name="end">End point of the line</param>
      /// <returns>bool  true=line exists, false=line is clipped away</returns>
      private static bool ClippAtTop(double top, ref Point start, ref Point end)
      {
         if ((start.Y > top) && (end.Y > top))
         {
            return false;
         }
         else if ((start.Y <= top) && (end.Y <= top))
         {
            return true;
         }
         else
         {
            if (start.Y > end.Y)
            {
               var help = start;
               start = end;
               end = help;
            }
            double qy = (top - start.Y) / (end.Y - start.Y);
            double deltax = qy * (end.X - start.X);
            end = new Point(start.X + deltax, top);
         }
         return true;
      }


      /// <summary>
      /// Byte size of the pixel.
      /// </summary>
      private const int M_PixelSize = 4;


      /// <summary>
      /// Byte buffer of canvas.
      /// </summary>
      private uint[] m_buffer = null;


      /// <summary>
      /// Byte buffer of canvas background color.
      /// </summary>
      private uint[] m_clearBuffer = null;


      /// <summary>
      /// Byte buffer of maximum height per column 
      /// </summary>
      private int[] m_ybuffer = null;


      /// <summary>
      /// Clear buffer of maximum height per column 
      /// </summary>
      private int[] m_clearYBuffer = null;

      
      /// <summary>
      /// Pen color for lines.
      /// </summary>
      uint m_penColor = 0;


      /// <summary>
      /// Pixel width of the buffer.
      /// </summary>
      private int m_width;


      /// <summary>
      /// Pixel height of the buffer.
      /// </summary>
      private int m_height;


      /// <summary>
      /// Width for byte alignment.
      /// </summary>
      private int m_alignWidth;
   }
}
