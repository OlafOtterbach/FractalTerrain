/// <summary>Definition of the class MainWindowBehaviour.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.ViewModel;
using System;
using System.Windows;
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
