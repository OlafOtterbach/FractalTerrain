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

      public event PropertyChangedEventHandler PropertyChanged;

      public static readonly DependencyProperty m_minimum = DependencyProperty.Register("AppleManMinimum", typeof(double), typeof(AppleManViewControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged));
      public double AppleManMinimum
      {
         get
         {
            return (double)GetValue(m_minimum);
         }
         set
         {
            SetValue(m_minimum, value);
         }
      }

      public static readonly DependencyProperty m_maximum = DependencyProperty.Register("AppleManMaximum", typeof(double), typeof(AppleManViewControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged));
      public double AppleManMaximum
      {
         get
         {
            return (double)GetValue(m_maximum);
         }
         set
         {
            SetValue(m_maximum, value);
         }
      }

      public static readonly DependencyProperty m_appleManMaximumSize = DependencyProperty.Register("AppleManMaximumSize", typeof(double), typeof(AppleManViewControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged));
      public double AppleManMaximumSize
      {
         get
         {
            return (double)GetValue(m_appleManMaximumSize);
         }
         set
         {
            SetValue(m_appleManMaximumSize, value);
         }
      }

      public static readonly DependencyProperty m_appleManSize = DependencyProperty.Register("AppleManSize", typeof(double), typeof(AppleManViewControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged));
      public double AppleManSize
      {
         get
         {
            return (double)GetValue(m_appleManSize);
         }
         set
         {
            SetValue(m_appleManSize, value);
         }
      }

      public static readonly DependencyProperty m_appleManXStartPosition = DependencyProperty.Register("AppleManXStartPosition", typeof(double), typeof(AppleManViewControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged));
      public double AppleManXStartPosition
      {
         get
         {
            return (double)GetValue(m_appleManXStartPosition);
         }
         set
         {
            SetValue(m_appleManXStartPosition, value);
         }
      }

      public static readonly DependencyProperty m_appleManYStartPosition = DependencyProperty.Register("AppleManYStartPosition", typeof(double), typeof(AppleManViewControl), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyPropertyChanged));
      public double AppleManYStartPosition
      {
         get
         {
            return (double)GetValue(m_appleManYStartPosition);
         }
         set
         {
            SetValue(m_appleManYStartPosition, value);
         }
      }

      
      private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var control = d as AppleManViewControl;
         if( control != null )
         {
            var name = e.Property.Name;
            control.OnAppleSizeChanged(name);
         }
      }

      private void OnAppleSizeChanged( string propertyName )
      {
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         var scroll = SizeScrollbar;
      }

      private void OnPropertyChanged(string name)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if( handler != null )
         {
            handler(this, new PropertyChangedEventArgs(name));
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
