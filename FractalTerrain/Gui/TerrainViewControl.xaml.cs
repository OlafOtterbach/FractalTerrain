/// <summary>Definition of the class TerrainViewControl.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.View;
using FractalTerrain.ViewModel;
using System.ComponentModel;
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



      private static DependencyProperty ElementActualWidthProperty
         = DependencyProperty.Register( "ElementActualWidth",
                                        typeof( double ),
                                        typeof( TerrainViewControl ),
                                        new FrameworkPropertyMetadata
                                        (
                                            default( double ),
                                            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                            OnActualWidthChanged
                                        )
                                      );
      public double ElementActualWidth
      {
         get { return (double)GetValue( ElementActualWidthProperty ); }
         set { var a = 0; a++; }
      }
      private void SetActualWidth( double value )
      {
         SetValue( ElementActualWidthProperty, value );
      }
      private static void OnActualWidthChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
      {
         var control = d as TerrainViewControl;
         if ( control != null )
         {
            control.SetActualWidth( control.ActualWidth );
         }
      }

      public static readonly DependencyProperty m_camera = DependencyProperty.Register( "Camera", typeof( CameraSettings ), typeof( TerrainViewControl ), new FrameworkPropertyMetadata( default(CameraSettings), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged ) );
      public CameraSettings Camera
      {
         get
         {
            return (CameraSettings)GetValue( m_camera );
         }
         set
         {
            SetValue( m_camera, value );
            SetCamera( value );
         }
      }
      private static void OnDependencyPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
      {
         var control = d as TerrainViewControl;
         if ( control != null )
         {
            var settings = control.GetValue( m_camera ) as CameraSettings;
            control.SetCamera( settings );            
         }
      }

      private void SetCamera( CameraSettings settings )
      {
         m_view3D.Camera.SetCamera( settings.AngleAxisEz, settings.AngleAxisEy, settings.Distance );
      }

      private void WriteCameraSettings()
      {
         Camera = new CameraSettings { AngleAxisEz = m_view3D.Camera.AngleAxisEz, AngleAxisEy = m_view3D.Camera.AngleAxisEy, Distance = m_view3D.Camera.Distance };
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
                  WriteCameraSettings();
                  m_view3D.Render();
               }
            }
            if( ( inputEvent.RightButton == MouseButtonState.Pressed ) && ( inputEvent.LeftButton == MouseButtonState.Released ) )
            {
               dy = -( (double)deltaY ) * 10.0;
               if( m_view3D != null )
               {
                  m_view3D.Camera.Zoom(dy);
                  WriteCameraSettings();
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
