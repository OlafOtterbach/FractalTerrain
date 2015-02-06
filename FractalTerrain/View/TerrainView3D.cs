/// <summary>Definition of the class TerrainView3D.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

namespace FractalTerrain.View
{
   /// <summary>
   /// View to show the fractal terrain.
   /// </summary>
   public class TerrainView3D
   {
      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="canvas">Canvas to draw on</param>
      public TerrainView3D(ICanvas2D canvas)
      {
         Camera = new ViewCamera();
         Camera.SetCamera(45.0, 25.0, 150.0);
         Camera.NearPlane = 1.0;

         m_canvas = canvas;
      }


      /// <summary>
      /// ViewModel for model displaying in view.
      /// </summary>
      public TerrainViewModel ViewModel { get; set; }

      public ViewCamera Camera { get; set; }


      /// <summary>
      /// Sets the pen for drawing.
      /// </summary>
      /// <param name="red"></param>
      /// <param name="green"></param>
      /// <param name="blue"></param>
      public void SetPen( int red, int green, int blue )
      {
         m_canvas.SetPen( red, green, blue );
      }


      public void Resize()
      {
         m_canvas.Resize();
      }


      public void Update(VisualTerrainModel visualModel)
      {
         if (visualModel == null)
         {
            return;
         }
         var middle = visualModel.Minimum + (visualModel.Maximum - visualModel.Minimum) / 2.0;
         Camera.MoveTo(middle);
      }


      public void Render(VisualTerrainModel visualModel)
      {
         DrawTerrain(visualModel);
         m_canvas.Refresh();
      }


      private void DrawTerrain(VisualTerrainModel visualModel)
      {
         if ((visualModel == null) || (!visualModel.IsValid))
         {
            return;
         }
         m_canvas.Clear();
         visualModel.GetGeometryLines(Camera).ToList().ForEach(DrawLine);
      }


      private void DrawLine(VisualLine line)
      {
         m_canvas.SetPen(line.Color);
         DrawLine(line.Start, line.End);
      }

      /// <summary>
      /// Draws a 3D line.
      /// </summary>
      /// <param name="start">Start point of the line</param>
      /// <param name="end">End point of the line</param>
      private void DrawLine( Point3D start, Point3D end )
      {
         var cameraFrame = Camera.Offset.Inverse();
         var frameStart = start * cameraFrame;
         var frameEnd = end * cameraFrame;
         if( ClippLineAtNearPlane(Camera.NearPlane, ref frameStart, ref frameEnd) )
         {
            double width = m_canvas.Width; ;
            double height = m_canvas.Height;
            var startPos = GetProjectionOfPoint(width, height, Camera.NearPlane, frameStart);
            var endPos = GetProjectionOfPoint(width, height, Camera.NearPlane, frameEnd);
            m_canvas.DrawLine(startPos, endPos);
         }
      }


      public static Point GetProjectionOfPoint(double canvasWidth, double canvasHeight, double nearPlaneDist, Point3D point)
      {
         // Create projection matrix
         double width = canvasWidth; ;
         double height = canvasHeight;
         double ratio = width / height;
         if( ratio <= 0.0 )
         {
            ratio = 1.0;
         }
         double ypos = point.Y;
         double xpos = point.X;
         double zpos = point.Z;
         double x = ( nearPlaneDist / ypos ) * xpos;
         double y = ( nearPlaneDist / ypos ) * zpos;
         var size = Math.Min(width, height);
         x = x * size + width / 2.0;
         y = y * size + height / 2.0;
         return new Point(x, y);
      }


      /// <summary>
      /// Clipps a line at the near plane of the view to visible size.
      /// </summary>
      /// <param name="nearPlaneDist">Distance of the nearplane to camera origin in camera Y-Axis</param>
      /// <param name="start">Startpoint of the line in camera space</param>
      /// <param name="end">Endpoint of the line in camera space</param>
      /// <returns>bool  true=line exists, false=line is clipped away</returns>
      public static bool ClippLineAtNearPlane(double nearPlaneDist, ref Point3D start, ref Point3D end)
      {
         if ((start.Y < nearPlaneDist) || (end.Y < nearPlaneDist))
         {
            if ((start.Y < nearPlaneDist) && (end.Y < nearPlaneDist))
            {
               return false;
            }
            var direction = end - start;
            var intersection = start;
            if (direction.Y != 0.0)
            {
               double lamda = Math.Abs((start.Y - nearPlaneDist) / direction.Y);
               intersection += direction * lamda;
            }
            if (start.Y < nearPlaneDist)
            {
               start = intersection;
            }
            else
            {
               end = intersection;
            }
         }
         return true;
      }


      /// <summary>
      /// Canvas of the view for drawing on.
      /// </summary>
      private ICanvas2D m_canvas;
   }
}
