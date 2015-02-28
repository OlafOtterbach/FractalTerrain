/// <summary>Definition of the class VisualTerrainModel.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System.Windows.Media.Media3D;

namespace FractalTerrain.View
{
   public class VisualTerrainModel
   {
      public VisualTerrainModel()
      {
         HeightFactor = 100.0;
      }

      public Point3D Minimum { get; set; }

      public Point3D Maximum { get; set; }

      public double HeightFactor { get; set; }

      public bool IsValid { get { return ( Vertices != null ); } }

      public VisualVertex[] Vertices{get; set;}

      public int MapSize { get; set; }
   }
}
