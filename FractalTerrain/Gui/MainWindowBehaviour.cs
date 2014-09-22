/// <summary>Definition of the class MainWindowBehaviour.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace FractalTerrain.Gui
{
   public class MainWindowBehaviour : Behavior<UIElement>
   {
      protected override void OnAttached()
      {
         var mainWindow = AssociatedObject as Window;
         mainWindow.ContentRendered += new EventHandler(OnContentRendered);
      }

      private void OnContentRendered(object sender, EventArgs e)
      {
         var mainWindow = AssociatedObject as Window;
         var viewModel = mainWindow.DataContext as TerrainViewModel;
         viewModel.OnStart();
      }
   }
}
