/// <summary>Definition of the class AppleManViewControl.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.View;
using FractalTerrain.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FractalTerrain.Gui
{
   public partial class AppleManViewControl : UserControl
   {
      public AppleManViewControl()
      {
         InitializeComponent();
         var parent = Application.Current.MainWindow;
         m_viewModel = parent.DataContext as TerrainViewModel;

         var appleImage = this.AppleImage;
         var appleManView = new AppleManViewWpf(appleImage);
         m_viewModel.Register( appleManView );
         appleManView.ViewModel = m_viewModel;

         appleImage.MouseMove += new System.Windows.Input.MouseEventHandler(OnMouseMove);
         appleImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseDown);
      }

      public static readonly DependencyProperty AppleManSettingsProperty = DependencyProperty.Register( "AppleManSettings", typeof( AppleManSettings ), typeof( AppleManViewControl ), new FrameworkPropertyMetadata( null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
      public AppleManSettings AppleManSettings
      {
         get
         {
            return (AppleManSettings)GetValue( AppleManSettingsProperty );
         }
         set
         {
            SetValue( AppleManSettingsProperty, value );
         }
      }
      private static void OnAppleManSettingsChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
      {
         var control = d as AppleManViewControl;
         if ( control != null )
         {
            control.SetAppleManSettings( control.AppleManSettings );
         }
      }
      private void SetAppleManSettings( AppleManSettings settings )
      {
         AppleManMinimum = settings.AppleManMinimalPosition;
      }


      public static readonly DependencyProperty AppleManMinimumProperty = DependencyProperty.Register( "AppleManMinimum", typeof( double ), typeof( AppleManViewControl ), new FrameworkPropertyMetadata( 1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
      public double AppleManMinimum
      {
         get
         {
            return (double)GetValue( AppleManMinimumProperty );
         }
         set
         {
            SetValue( AppleManMinimumProperty, value );
         }
      }

      public static readonly DependencyProperty AppleManMaximumProperty = DependencyProperty.Register( "AppleManMaximum", typeof( double ), typeof( AppleManViewControl ), new FrameworkPropertyMetadata( 1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
      public double AppleManMaximum
      {
         get
         {
            return (double)GetValue( AppleManMaximumProperty );
         }
         set
         {
            SetValue( AppleManMaximumProperty, value );
         }
      }

      public static readonly DependencyProperty AppleManMaximumSizeProperty = DependencyProperty.Register( "AppleManMaximumSize", typeof( double ), typeof( AppleManViewControl ), new FrameworkPropertyMetadata( 1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
      public double AppleManMaximumSize
      {
         get
         {
            return (double)GetValue( AppleManMaximumSizeProperty );
         }
         set
         {
            SetValue( AppleManMaximumSizeProperty, value );
         }
      }

      public static readonly DependencyProperty AppleManSizeProperty = DependencyProperty.Register( "AppleManSize", typeof( double ), typeof( AppleManViewControl ), new FrameworkPropertyMetadata( 1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
      public double AppleManSize
      {
         get
         {
            return (double)GetValue( AppleManSizeProperty );
         }
         set
         {
            SetValue( AppleManSizeProperty, value );
         }
      }

      public static readonly DependencyProperty AppleManXStartPositionProperty = DependencyProperty.Register( "AppleManXStartPosition", typeof( double ), typeof( AppleManViewControl ), new FrameworkPropertyMetadata( 1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
      public double AppleManXStartPosition
      {
         get
         {
            return (double)GetValue( AppleManXStartPositionProperty );
         }
         set
         {
            SetValue( AppleManXStartPositionProperty, value );
         }
      }

      public static readonly DependencyProperty AppleManYStartPositionProperty = DependencyProperty.Register( "AppleManYStartPosition", typeof( double ), typeof( AppleManViewControl ), new FrameworkPropertyMetadata( 1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault ) );
      public double AppleManYStartPosition
      {
         get
         {
            return (double)GetValue( AppleManYStartPositionProperty );
         }
         set
         {
            SetValue( AppleManYStartPositionProperty, value );
         }
      }

      private void OnMouseDown(object sender, MouseButtonEventArgs inputEvent)
      {
         m_mousePosition = inputEvent.GetPosition((UIElement)sender);
      }

      private void OnMouseMove(object t_sender, MouseEventArgs inputEvent)
      {
         if( ( inputEvent.LeftButton == MouseButtonState.Pressed ) || ( inputEvent.RightButton == MouseButtonState.Pressed ) )
         {
            var position = inputEvent.GetPosition((UIElement)t_sender);
            var delta = position - m_mousePosition;
            m_mousePosition = position;
            if( ( inputEvent.LeftButton == MouseButtonState.Pressed ) && ( inputEvent.RightButton == MouseButtonState.Released ) )
            {
               Move(delta);
            }
            if( ( inputEvent.RightButton == MouseButtonState.Pressed ) && ( inputEvent.LeftButton == MouseButtonState.Released ) )
            {
               Zoom(delta);
            }
         }
      }

      private void Move(Vector deltaMovement)
      {
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         var appleImage = this.AppleImage;

         var size = viewModel.AppleManSize;
         var width = appleImage.ActualWidth;
         var height = appleImage.ActualHeight;
         var deltaX = deltaMovement.X * ( size / width );
         var deltaY = deltaMovement.Y * ( size / height );
         var xpos = viewModel.AppleManXStartPosition - deltaX;
         var ypos = viewModel.AppleManYStartPosition - deltaY;

         viewModel.AppleManXStartPosition = xpos;
         viewModel.AppleManYStartPosition = ypos;
      }

      private void Zoom(Vector deltaZoom)
      {
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         const double factor = 0.1;
         var sign = deltaZoom.Y > 0.0 ? 1.0 : -1.0;
         var size = viewModel.AppleManSize;
         size = size * ( 1.0 - sign * factor );
         viewModel.AppleManSize = size;
      }

      private Point m_mousePosition;

      private TerrainViewModel m_viewModel;
   }
}
