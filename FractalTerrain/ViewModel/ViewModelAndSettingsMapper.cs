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
            CameraTopLeft = new CameraSettings( viewModel.CameraTopLeft ),
            CameraTopRight = new CameraSettings( viewModel.CameraTopRight ),
            CameraBottomLeft = new CameraSettings( viewModel.CameraBottomLeft ),
            CameraBottomRight = new CameraSettings( viewModel.CameraBottomRight ),
            CameraSetting = new CameraSettings( viewModel.CameraSetting )
         };
        
         return settings;
      }

      public ViewModelSettings MapSettingsToViewModel( ViewModelSettings settings, TerrainViewModel viewModel )
      {
         viewModel.CameraTopLeft = new CameraSettings( settings.CameraTopLeft );
         viewModel.CameraTopRight = new CameraSettings( settings.CameraTopRight );
         viewModel.CameraBottomLeft = new CameraSettings( settings.CameraBottomLeft );
         viewModel.CameraBottomRight = new CameraSettings( settings.CameraBottomRight );
         viewModel.CameraSetting = new CameraSettings( settings.CameraSetting );
         viewModel.ColumnRatio = new System.Windows.GridLength(settings.HoricontalRatio, System.Windows.GridUnitType.Star );
         viewModel.RowRatio = new System.Windows.GridLength( settings.VerticalRatio, System.Windows.GridUnitType.Star );
         return settings;
      }
   }
}
