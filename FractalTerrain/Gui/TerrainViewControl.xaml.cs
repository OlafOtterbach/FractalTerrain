/// <summary>Definition of the class TerrainViewControl.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.View;
using FractalTerrain.ViewModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FractalTerrain.Gui
{
   /// <summary>
   /// Interaction logic for TerrainViewControl.xaml
   /// </summary>
   public partial class TerrainViewControl : UserControl, INotifyPropertyChanged
   {
      public TerrainViewControl()
      {
         InitializeComponent();
         InitView();
      }

      private bool m_busy;

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string name)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null)
         {
            handler(this, new PropertyChangedEventArgs(name));
         }
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

      public bool ActiveClock
      {
         get
         {
            return m_ActiveClock;
         }
         set
         {
            m_ActiveClock = value;
            OnPropertyChanged("ActiveClock");
         }
      }
      private bool m_ActiveClock;

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


      private static DependencyProperty ElementActualWidthProperty
         = DependencyProperty.Register( "ElementActualWidth",
                                        typeof( double ),
                                        typeof( TerrainViewControl ),
                                        new FrameworkPropertyMetadata
                                        (
                                            default( double ),
                                            OnActualWidthChanged
                                        )
                                      );
      public double ElementActualWidth
      {
         get { return (double)GetValue( ElementActualWidthProperty ); }
         set { SetValue(ElementActualHeightProperty, value); }
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
                                            OnActualHeightChanged
                                        )
                                      );
      public double ElementActualHeight
      {
         get { return (double)GetValue( ElementActualHeightProperty ); }
         set { SetValue(ElementActualHeightProperty, value); }
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

      public static readonly DependencyProperty CameraProperty
         = DependencyProperty.Register("Camera", 
                                       typeof(CameraSettings),
                                       typeof(TerrainViewControl),
                                       new FrameworkPropertyMetadata
                                          (default(CameraSettings), 
                                           FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                                           OnCameraPropertyChanged));
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
         m_view3D.Camera.SetCamera(settings.AngleAxisEz, settings.AngleAxisEy, settings.Distance);
         RenderView(VisualModel);
      }


      private async void RenderView(VisualTerrainModel visualModel)
      {
         if (m_syncronized)
         {
            m_view3D.Render(visualModel);
            m_view3D.Redraw();
         }
         else
         {
            ActiveClock = true;
            bool repeat = false;
            do
            {
               repeat = false;
               Task future = Task.Factory.StartNew(() => m_view3D.Render(visualModel));
               await future;
               if(ResizeRequest)
               {
                  m_view3D.Resize();
                  SetActualWidth(ActualWidth);
                  SetActualHeight(ActualHeight);
                  repeat = true;
               }
            }
            while (repeat);
            ActiveClock = false;
            m_view3D.Redraw();
         }
      }


      private void WriteCameraSettings()
      {
         m_syncronized = true;
         Camera = new CameraSettings { AngleAxisEz = m_view3D.Camera.AngleAxisEz, AngleAxisEy = m_view3D.Camera.AngleAxisEy, Distance = m_view3D.Camera.Distance };
         m_syncronized = false;
      }


      private void OnResize(object t_sender, SizeChangedEventArgs t_event)
      {
         if( m_view3D != null )
         {
            if (!ActiveClock)
            {
               m_view3D.Resize();
               RenderView(VisualModel);
               SetActualWidth(ActualWidth);
               SetActualHeight(ActualHeight);
            }
            else 
            {
               ResizeRequest = true;
            }
         }
      }

      private bool ResizeRequest
      {
         get
         {
            var toResize = m_resizeRequest;
            m_resizeRequest = false;
            return toResize;
         }
         set
         {
            if(value)
            {
               m_resizeRequest = value;
            }
         }
      }
      private bool m_resizeRequest;

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

      private double m_mouseX = 0;

      private double m_mouseY = 0;

      private bool m_syncronized = false;
   }
}
