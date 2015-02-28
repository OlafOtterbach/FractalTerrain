/// <summary>Definition of the class DataV1Dot1.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.ViewModel;

namespace FractalTerrain.Persistence
{
   public class DataV1Dot1 : IData
   {
      public DataV1Dot1()
      {
         FileType = "FractalTerrain";
         Version = "V1.1";
      }

      // Header
      public string FileType { get; private set; }

      public string Version { get; private set; }

      // Data
      public int MapSize { get; set; }

      public double AppleManXStartPosition { get; set; }

      public double AppleManYStartPosition { get; set; }

      public double AppleManSize { get; set; }

      public double AppleManMinimalPosition { get; set; }

      public double AppleManMaximalPosition { get; set; }

      public double AppleManMinimalSize { get; set; }

      // View settings
      public CameraSettings CameraTopLeft { get; set; }

      public CameraSettings CameraTopRight { get; set; }
      
      public CameraSettings CameraBottomLeft { get; set; }
      
      public CameraSettings CameraBottomRight { get; set; }
      
      public CameraSettings CameraSetting { get; set; }

      public double HoricontalRatio { get; set; }

      public double VerticalRatio { get; set; }
   }
}
