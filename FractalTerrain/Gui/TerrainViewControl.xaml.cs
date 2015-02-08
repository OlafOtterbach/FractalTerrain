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
         var canvas2D = new Canvas2DWpf(this.ControlCanvas);
         m_view3D = new TerrainView3D(canvas2D);

         this.ControlCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(OnMouseMove);
         this.ControlCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseDown);
         this.ControlCanvas.SizeChanged += new SizeChangedEventHandler(OnResize);
      }


      public static readonly DependencyProperty VisualModelProperty = DependencyProperty.Register("VisualModel", typeof(VisualTerrainModel), typeof(TerrainViewControl), new FrameworkPropertyMetadata(default(CameraSettings), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender, OnCameraPropertyChanged));
      public VisualTerrainModel VisualModel
      {
         get
         {
            return (VisualTerrainModel)GetValue(VisualModelProperty);
         }
         set
         {
            SetValue(VisualModelProperty, value);
         }
      }
      private static void OnVisualModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var control = d as TerrainViewControl;
         if (control != null)
         {
            //control.Update(control.VisualModel);
         }
      }

      private void Update( VisualTerrainModel visualModel )
      {
         m_view3D.Update(visualModel);
         m_view3D.Render(visualModel);
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


      private static DependencyProperty ElementActualHeightProperty
         = DependencyProperty.Register( "ElementActualHeight",
                                        typeof( double ),
                                        typeof( TerrainViewControl ),
                                        new FrameworkPropertyMetadata
                                        (
                                            default( double ),
                                            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                            OnActualHeightChanged
                                        )
                                      );
      public double ElementActualHeight
      {
         get { return (double)GetValue( ElementActualHeightProperty ); }
         set { var a = 0; a++; }
      }
      private void SetActualHeight( double value )
      {
         SetValue( ElementActualHeightProperty, value );
      }
      private static void OnActualHeightChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
      {
         var control = d as TerrainViewControl;
         if ( control != null )
         {
            control.SetActualHeight( control.ActualHeight );
         }
      }

      public static readonly DependencyProperty CameraProperty = DependencyProperty.Register("Camera", typeof(CameraSettings), typeof(TerrainViewControl), new FrameworkPropertyMetadata(default(CameraSettings), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender, OnCameraPropertyChanged));
      public CameraSettings Camera
      {
         get
         {
            return (CameraSettings)GetValue( CameraProperty );
         }
         set
         {
            SetValue( CameraProperty, value );
         }
      }
      private static void OnCameraPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var control = d as TerrainViewControl;
         if ( control != null )
         {
            var settings = control.GetValue( CameraProperty ) as CameraSettings;
            control.SetCamera( settings );
         }
      }

      private void SetCamera( CameraSettings settings )
      {
         m_view3D.Camera.SetCamera( settings.AngleAxisEz, settings.AngleAxisEy, settings.Distance );
         m_view3D.Render(VisualModel);
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
            m_view3D.Render(VisualModel);
            SetActualWidth( ActualWidth );
            SetActualHeight( ActualHeight );
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
               }
            }
            if( ( inputEvent.RightButton == MouseButtonState.Pressed ) && ( inputEvent.LeftButton == MouseButtonState.Released ) )
            {
               dy = -( (double)deltaY ) * 10.0;
               if( m_view3D != null )
               {
                  m_view3D.Camera.Zoom(dy);
                  WriteCameraSettings();
               }
            }
         }
      }

      private TerrainView3D m_view3D;

      double m_mouseX = 0;

      double m_mouseY = 0;
   }
}
