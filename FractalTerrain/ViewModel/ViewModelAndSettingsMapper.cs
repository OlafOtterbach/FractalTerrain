using System.Collections.Generic;
using System.Windows;

namespace FractalTerrain.ViewModel
{
   public class ViewModelAndSettingsMapper
   {
      public ViewModelSettings CreateSettingsFromViewModel( TerrainViewModel viewModel )
      {
         var settings = new ViewModelSettings
         {
            CameraTopLeft = new CameraSettings( viewModel.CameraTopLeft ),
            CameraTopRight = new CameraSettings( viewModel.CameraTopRight ),
            CameraBottomLeft = new CameraSettings( viewModel.CameraBottomLeft ),
            CameraBottomRight = new CameraSettings( viewModel.CameraBottomRight ),
            CameraSetting = new CameraSettings( viewModel.CameraSetting ),
            HoricontalRatio = viewModel.WidthLeft / viewModel.WidthRight,
            VerticalRatio = viewModel.HeightTop / viewModel.HeightBottom,
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
         viewModel.ColumnRatio = new GridLength(settings.HoricontalRatio, System.Windows.GridUnitType.Star );
         viewModel.RowRatio = new GridLength( settings.VerticalRatio, System.Windows.GridUnitType.Star );
         return settings;
      }
   }
}
