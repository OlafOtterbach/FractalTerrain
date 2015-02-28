/// <summary>Definition of the class ViewModelSettings.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.ViewModel
{
   public class ViewModelSettings
   {
      public ViewModelSettings()
      {
         CameraTopLeft = new CameraSettings();
         CameraTopRight = new CameraSettings();
         CameraBottomLeft = new CameraSettings();
         CameraBottomRight = new CameraSettings();
         CameraSetting = new CameraSettings();
         HoricontalRatio = 1.0;
         VerticalRatio = 1.0;
      }

      public CameraSettings CameraTopLeft { get; set; }

      public CameraSettings CameraTopRight { get; set; }

      public CameraSettings CameraBottomLeft { get; set; }

      public CameraSettings CameraBottomRight { get; set; }

      public CameraSettings CameraSetting { get; set; }

      public double HoricontalRatio { get; set; }

      public double VerticalRatio { get; set; }
   }
}
