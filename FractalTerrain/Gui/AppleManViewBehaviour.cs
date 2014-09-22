using FractalTerrain.View;
using FractalTerrain.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FractalTerrain.Gui
{
   public class AppleManViewBehaviour : Behavior<UIElement>
   {
      protected override void OnAttached()
      {
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         var appleImage = AssociatedObject as Image;
         var appleManView = new AppleManViewWpf(appleImage);
         viewModel.AppleView = appleManView;
         appleManView.ViewModel = viewModel;

         appleImage.MouseMove += new System.Windows.Input.MouseEventHandler(OnMouseMove);
         appleImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseDown);
      }

      public void OnMouseDown(object sender, MouseButtonEventArgs inputEvent)
      {
         m_mousePosition = inputEvent.GetPosition((UIElement)sender);
      }

      public void OnMouseMove(object t_sender, MouseEventArgs inputEvent)
      {
         if( ( inputEvent.LeftButton == MouseButtonState.Pressed ) || ( inputEvent.RightButton == MouseButtonState.Pressed ) )
         {
            var position = inputEvent.GetPosition((UIElement)t_sender);
            var delta = position - m_mousePosition;
            m_mousePosition = position;
            if( ( inputEvent.LeftButton == MouseButtonState.Pressed ) && ( inputEvent.RightButton == MouseButtonState.Released ) )
            {
               Move( delta );
            }
            if( ( inputEvent.RightButton == MouseButtonState.Pressed ) && ( inputEvent.LeftButton == MouseButtonState.Released ) )
            {
               Zoom( delta );
            }
         }
      }

      private void Move( Vector deltaMovement )
      {
         var appleImage = AssociatedObject as Image;
         var width = appleImage.ActualWidth;
         var height = appleImage.ActualHeight;
         var ratio = width / height;
         var deltaY = -ratio * deltaMovement.Y / height * 100.0;
         var deltaX = -deltaMovement.X / width * 100.0;
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         var xpos = viewModel.AppleManXStartPosition + deltaX;
         var ypos = viewModel.AppleManYStartPosition + deltaY;
         viewModel.AppleManXStartPosition = xpos;
         viewModel.AppleManYStartPosition = ypos;
      }


      private void Zoom( Vector deltaZoom )
      {
         const double factor = 0.1;
         var length = deltaZoom.Length;
         var sign = deltaZoom.Y > 0.0 ? 1.0 : -1.0;
         var zoom = sign * factor * length;
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         viewModel.AppleManSize += zoom;
      }

      private Point m_mousePosition;
   }
}
