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
         mainWindow.SizeChanged += new SizeChangedEventHandler( this.OnResize );
         mainWindow.ContentRendered += new EventHandler(OnContentRendered);
      }

      private void OnContentRendered(object sender, EventArgs e)
      {
         var mainWindow = AssociatedObject as Window;
         var viewModel = mainWindow.DataContext as TerrainViewModel;
         viewModel.OnStart();
      }

      private void OnResize(object t_sender, RoutedEventArgs t_event)
      {
         var mainWindow = AssociatedObject as Window;
         var viewModel = mainWindow.DataContext as TerrainViewModel;
         viewModel.Resize();
      }
   }
}
