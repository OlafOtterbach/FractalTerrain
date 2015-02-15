/// <summary>Definition of the class TerrainView3D.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
         m_geometry = new List<VisualLine>();
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


      public void Render(VisualTerrainModel visualModel)
      {
         if (visualModel == null)
         {
            return;
         }
         var middle = visualModel.Minimum + (visualModel.Maximum - visualModel.Minimum) / 2.0;
         Camera.MoveTo(middle);
         var geometry = GetGeometryLines(visualModel, Camera).ToList();
         m_canvas.Clear();
         geometry.ForEach(DrawLine);
      }

      public void Redraw()
      {
         m_canvas.Refresh();
      }


     private IEnumerable<VisualLine> GetGeometryLines(VisualTerrainModel visualModel, ViewCamera camera)
      {
         var mapSize = visualModel.MapSize;
         var vertices = visualModel.Vertices;
         var cameraOffset = camera.Offset.Offset();
         var offset = new Point3D(cameraOffset.X, cameraOffset.Y, 0.0);
         var max = mapSize - 1;
         var positions = new List<VectorInt>() { new VectorInt(0, 0), new VectorInt(max, 0), new VectorInt(0, max), new VectorInt(max, max) };
         var positionAndVertex = positions.Select(pos => new { Position = pos, Vertex = vertices[pos.Y * mapSize + pos.X].Vertex }).ToList();
         var positionAndDistance = positionAndVertex.Select(x => new { Position = x.Position, Distance = (x.Vertex - offset).Length }).OrderBy(x => x.Distance).ToList();
         var start = positionAndDistance[0].Position;
         var xEnd = positionAndDistance[1].Position;
         var p3 = positionAndDistance[2].Position;
         var p4 = positionAndDistance[3].Position;
         var xDirection = (xEnd - start).Normalized;
         var yDirection = (p3 - start).Normalized;
         if ((yDirection.X != 0) && (yDirection.Y != 0))
         {
            yDirection = (p4 - start).Normalized;
         }
         return GetLines(visualModel,start, xDirection, yDirection);
      }


      private IEnumerable<VisualLine> GetLines(VisualTerrainModel visualModel,VectorInt start, VectorInt xDirectionVector, VectorInt yDirectionVector)
      {
         var mapSize = visualModel.MapSize;
         var vertices = visualModel.Vertices;
         var xDirection = GetDirection(xDirectionVector);
         var yDirection = GetDirection(yDirectionVector);
         var count = mapSize * mapSize * 2;
         var lines = new VisualLine[count];
         Parallel.For(0, mapSize, y =>
         {
            var pos = start + y * yDirectionVector;
            for (int x = 0; x < mapSize; x++)
            {
               var index = pos.Y * mapSize + pos.X;
               var lineIndex = 2 * (y * mapSize + x);
               lines[lineIndex] = (vertices[index].GetLineOfDirection(xDirection));
               lines[lineIndex + 1] = (vertices[index].GetLineOfDirection(yDirection));
               pos = pos + xDirectionVector;
            };
         });
         var validLines = lines.Where(x => x != null).ToList();
         return validLines;
      }


      private VisualVertex.Direction GetDirection(VectorInt directionVector)
      {
         var direction = VisualVertex.Direction.e_east;
         var x = directionVector.X;
         var y = directionVector.Y;
         if (x != 0)
         {
            direction = (x >= 1) ? VisualVertex.Direction.e_east : VisualVertex.Direction.e_west;
         }
         else
         {
            direction = (y >= 1) ? VisualVertex.Direction.e_north : VisualVertex.Direction.e_south;
         }
         return direction;
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

      List<VisualLine> m_geometry;
   }
}
