using System.Collections.Generic;
namespace FractalTerrain.ViewModel
{
   public class ViewModelAndSettingsMapper
   {
      public ViewModelSettings MapViewModelToSettings( TerrainViewModel viewModel )
      {
         var settings = new ViewModelSettings
         {
            CameraSettings = new List<CameraSettings>()
         };
         
         return settings;
      }
   }
}
