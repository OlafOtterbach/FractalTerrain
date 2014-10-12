/// <summary>Definition of the class TerrainViewControl.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.View;
using FractalTerrain.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
         InitView();
      }

      private void InitView()
      {
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         var canvas2D = new Canvas2DWpf(this.ControlCanvas);
         m_view3D = new TerrainView3D(canvas2D);
         viewModel.Register(m_view3D);
         m_view3D.ViewModel = viewModel;

         this.ControlCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(OnMouseMove);
         this.ControlCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseDown);
         this.ControlCanvas.SizeChanged += new SizeChangedEventHandler(OnResize);
      }

      private void OnResize(object t_sender, SizeChangedEventArgs t_event)
      {
         if( m_view3D != null )
         {
            m_view3D.Resize();
            m_view3D.Render();
         }
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
               if( m_view3D != null )
               {
                  m_view3D.Camera.OrbitYZ(dy);
                  m_view3D.Camera.OrbitXY(dx);
                  m_view3D.Render();
               }
            }
            if( ( inputEvent.RightButton == MouseButtonState.Pressed ) && ( inputEvent.LeftButton == MouseButtonState.Released ) )
            {
               dy = -( (double)deltaY ) * 10.0;
               if( m_view3D != null )
               {
                  m_view3D.Camera.Zoom(dy);
                  m_view3D.Render();
               }
            }
         }
      }

      private TerrainView3D m_view3D;

      double m_mouseX = 0;

      double m_mouseY = 0;
   }
}
