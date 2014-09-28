using FractalTerrain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FractalTerrain.Gui
{
   /// <summary>
   /// Interaction logic for TerrainViewControl.xaml
   /// </summary>
   public partial class TerrainViewControl : UserControl
   {
      public TerrainViewControl()
      {
         InitializeComponent();
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;

         var terrainCanvas = this.ControlImage;
         terrainCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(OnMouseMove);
         terrainCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseDown);
         terrainCanvas.SizeChanged += new SizeChangedEventHandler(OnResize);
      }

      private void OnResize(object t_sender, SizeChangedEventArgs t_event)
      {
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         var view3D = viewModel.View;
         view3D.Resize(); view3D.Render();
      }


      public void OnMouseDown(object sender, MouseButtonEventArgs inputEvent)
      {
         Point point = inputEvent.GetPosition((UIElement)sender);
         m_mouseX = point.X;
         m_mouseY = point.Y;
      }


      public void OnMouseMove(object t_sender, MouseEventArgs inputEvent)
      {
         if( ( inputEvent.LeftButton == MouseButtonState.Pressed ) || ( inputEvent.RightButton == MouseButtonState.Pressed ) )
         {
            var parent = Application.Current.MainWindow;
            var viewModel = parent.DataContext as TerrainViewModel;
            Point point = inputEvent.GetPosition((UIElement)t_sender);
            double deltaX = point.X - m_mouseX;
            double deltaY = point.Y - m_mouseY;
            m_mouseX = point.X;
            m_mouseY = point.Y;
            double dx = 0.0;
            double dy = 0.0;
            if( ( inputEvent.LeftButton == MouseButtonState.Pressed ) && ( inputEvent.RightButton == MouseButtonState.Released ) )
            {
               dx = ( (double)( -deltaX ) );
               dy = ( (double)( -deltaY ) );
 //              viewModel.Camera.OrbitYZ(dy);
 //              viewModel.Camera.OrbitXY(dx);
            }
            if( ( inputEvent.RightButton == MouseButtonState.Pressed ) && ( inputEvent.LeftButton == MouseButtonState.Released ) )
            {
               dy = -( (double)deltaY ) * 10.0;
 //              viewModel.Camera.Zoom(dy);
            }
         }
      }


      /// <summary>
      /// Aktuelle X-Mauskoordinate.
      /// </summary>
      double m_mouseX = 0;

      /// <summary>
      /// Aktuelle Y-Mauskoordinate.
      /// </summary>
      double m_mouseY = 0;
   }
}
