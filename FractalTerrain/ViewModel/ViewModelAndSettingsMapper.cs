using System.Collections.Generic;
namespace FractalTerrain.ViewModel
{
   public class ViewModelAndSettingsMapper
   {
      public ViewModelSettings CreateSettingsFromViewModel( TerrainViewModel viewModel )
      {
         var settings = new ViewModelSettings
         {
            HoricontalRatio = 0.5,
            VerticalRatio = 0.5,
            CameraTopLeft = new CameraSettings( viewModel.Camera1 ),
            CameraTopRight = new CameraSettings( viewModel.Camera2 ),
            CameraBottomLeft = new CameraSettings( viewModel.Camera3 ),
            CameraBottomRight = new CameraSettings( viewModel.Camera4 ),
            CameraSetting = new CameraSettings( viewModel.Camera5 )
         };
        
         return settings;
      }

      public ViewModelSettings MapSettingsToViewModel( ViewModelSettings settings, TerrainViewModel viewModel )
      {
         viewModel.Camera1 = new CameraSettings( settings.CameraTopLeft );
         viewModel.Camera2 = new CameraSettings( settings.CameraTopRight );
         viewModel.Camera3 = new CameraSettings( settings.CameraBottomLeft );
         viewModel.Camera4 = new CameraSettings( settings.CameraBottomRight );
         viewModel.Camera5 = new CameraSettings( settings.CameraSetting );
         viewModel.ColumnRatio = new System.Windows.GridLength(settings.HoricontalRatio, System.Windows.GridUnitType.Star );
         viewModel.RowRatio = new System.Windows.GridLength( settings.VerticalRatio, System.Windows.GridUnitType.Star );
         return settings;
      }
   }
}
