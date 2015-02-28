/// <summary>Definition of the class AppleManViewControl.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FractalTerrain.Gui
{
   public partial class AppleManViewControl : UserControl, INotifyPropertyChanged
   {
      public AppleManViewControl()
      {
         InitializeComponent();

         var appleImage = this.AppleImage;
         m_appleManView = new AppleManView(appleImage);
         appleImage.MouseMove += new System.Windows.Input.MouseEventHandler(OnMouseMove);
         appleImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseDown);
      }

      private AppleManView m_appleManView;

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged( string name )
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if ( handler != null )
         {
            handler( this, new PropertyChangedEventArgs( name ) );
         }
      }

      public static readonly DependencyProperty AppleManSettingsProperty = DependencyProperty.Register("AppleManSettings", typeof(AppleManSettings), typeof(AppleManViewControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender, OnAppleManSettingsChanged));
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
            control.UpdateControl();
         }
      }


      private void UpdateControl()
      {
         OnPropertyChanged("AppleManMinimalSize");
         OnPropertyChanged("AppleManMaximumSize");
         OnPropertyChanged("AppleManMinimum");
         OnPropertyChanged("AppleManMaximum");
         OnPropertyChanged("AppleManSize");
         OnPropertyChanged("AppleManXStartPosition");
         OnPropertyChanged("AppleManYStartPosition");
         m_appleManView.Update(AppleManSettings);
         m_appleManView.Render();
      }

      private void UpdateSettings()
      {
         AppleManSettings = new AppleManSettings(AppleManSettings);
      }

      public double AppleManMinimum
      {
         get
         {
            return AppleManSettings.AppleManMinimalPosition;
         }
         set
         {
            AppleManSettings.AppleManMinimalPosition = value;
            OnPropertyChanged( "AppleManMinimum" );
            UpdateSettings();
         }
      }

      public double AppleManMaximum
      {
         get
         {
            return AppleManSettings.AppleManMaximalPosition;
         }
         set
         {
            AppleManSettings.AppleManMaximalPosition = value;
            OnPropertyChanged( "AppleManMaximum" );
            UpdateSettings();
         }
      }

      public double AppleManMinimalSize 
      {
         get
         {
            return AppleManSettings.AppleManMinimalSize;
         }
         set
         {
            AppleManSettings.AppleManMinimalSize = value;
            OnPropertyChanged("AppleManMinimalSize");
            UpdateSettings();
         }
      }

      public double AppleManMaximumSize
      {
         get
         {
            return AppleManSettings.AppleManMaximalPosition + AppleManSettings.AppleManMinimalSize - AppleManSettings.AppleManMinimalPosition;
         }
      }

      public double AppleManSize
      {
         get
         {
            return AppleManSettings.AppleManSize;
         }
         set
         {
            AppleManSettings.AppleManSize = value;
            OnPropertyChanged( "AppleManSize" );
            UpdateSettings();
         }
      }

      public double AppleManXStartPosition
      {
         get
         {
            return AppleManSettings.AppleManXStartPosition;
         }
         set
         {
            AppleManSettings.AppleManXStartPosition = value;
            OnPropertyChanged( "AppleManXStartPosition" );
            UpdateSettings();
         }
      }

      public double AppleManYStartPosition
      {
         get
         {
            return AppleManSettings.AppleManYStartPosition;
         }
         set
         {
            AppleManSettings.AppleManYStartPosition = value;
            OnPropertyChanged( "AppleManYStartPosition" );
            UpdateSettings();
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
         var appleImage = this.AppleImage;
         var size = AppleManSize;
         var width = appleImage.ActualWidth;
         var height = appleImage.ActualHeight;
         var deltaX = deltaMovement.X * ( size / width );
         var deltaY = deltaMovement.Y * ( size / height );
         var xpos = AppleManXStartPosition - deltaX;
         var ypos = AppleManYStartPosition - deltaY;

         AppleManSettings.AppleManXStartPosition = xpos;
         AppleManSettings.AppleManYStartPosition = ypos;
         OnPropertyChanged( "AppleManXStartPosition" );
         OnPropertyChanged( "AppleManYStartPosition" );
         UpdateSettings();
      }

      private void Zoom(Vector deltaZoom)
      {
         const double factor = 0.1;
         var sign = deltaZoom.Y > 0.0 ? 1.0 : -1.0;
         var size = AppleManSettings.AppleManSize;
         size = size * ( 1.0 - sign * factor );
         AppleManSettings.AppleManSize = size;

         OnPropertyChanged( "AppleManSize" );
         UpdateSettings();
      }

      private Point m_mousePosition;
   }
}
