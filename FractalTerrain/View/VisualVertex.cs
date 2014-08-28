/// <summary>Definition of the struct VisualVertex.</summary>
/// <author>Olaf Otterbach</author>
/// <start>10.06.2014</start>
/// <state>10.06.2014</state>

using System.Collections.Generic;
using System.Windows.Media.Media3D;
namespace FractalTerrain
{
   /// <summary>
   /// Class for a vertex in a line grid.
   /// </summary>
   public class VisualVertex
   {
      public enum Direction
      {
         e_north = 0,
         e_west = 1,
         e_east = 2,
         e_south = 3
      }

      public VisualVertex()
      {
         m_direction = new List<VisualLine>() { null, null, null, null };
      }

      public Point3D Vertex { get; set; }


      public VisualLine GetLineOfDirection( Direction direction )
      {
         return m_direction[(int)direction];
      }


      public VisualLine North 
      { 
         set
         {
            m_direction[(int)Direction.e_north] = value;
         }
         get
         {
            return m_direction[(int)Direction.e_north];
         }
      }


      public VisualLine West
      {
         set
         {
            m_direction[(int)Direction.e_west] = value;
         }
         get
         {
            return m_direction[(int)Direction.e_west];
         }
      }


      public VisualLine East
      {
         set
         {
            m_direction[(int)Direction.e_east] = value;
         }
         get
         {
            return m_direction[(int)Direction.e_east];
         }
      }


      public VisualLine South
      {
         set
         {
            m_direction[(int)Direction.e_south] = value;
         }
         get
         {
            return m_direction[(int)Direction.e_south];
         }
      }

      private List<VisualLine> m_direction;
   }
}
