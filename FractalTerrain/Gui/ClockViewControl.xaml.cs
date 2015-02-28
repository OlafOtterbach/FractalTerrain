/// <summary>Definition of the class ClockViewControl.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FractalTerrain.Gui
{
   /// <summary>
   /// Interaction logic for ClockViewControl.xaml
   /// </summary>
   public partial class ClockViewControl : UserControl, INotifyPropertyChanged
   {
      public ClockViewControl()
      {
         InitializeComponent();
         ScaleFactor = 0.5;
      }

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string name)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null)
         {
            handler(this, new PropertyChangedEventArgs(name));
         }
      }

      public double ScaleFactor
      {
         get
         {
            return m_scaleFactor;
         }
         set
         {
            m_scaleFactor = value;
            OnPropertyChanged("ScaleFactor");
         }
      }

      public static readonly DependencyProperty SizeFactorProperty = DependencyProperty.Register("SizeFactor", typeof(double), typeof(ClockViewControl), new FrameworkPropertyMetadata(200.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSizeFactorChanged));
      public double SizeFactor
      {
         get
         {
            return (double)GetValue(SizeFactorProperty);
         }
         set
         {
            SetValue(SizeFactorProperty, value);
         }
      }
      private static void OnSizeFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var control = d as ClockViewControl;
         if (control != null)
         {
            control.SetSizeFactor((double)e.NewValue);
         }
      }

      private void SetSizeFactor( double sizeFactor )
      {
         const double originalSize = 200.0;
         var newSize = sizeFactor / 3.0;
         ScaleFactor = newSize / originalSize;
         Width = newSize + 5.0;
         Height = newSize + 5.0;
      }

      public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register( "IsActive", typeof( bool ), typeof( ClockViewControl ), new FrameworkPropertyMetadata( true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsActiveChanged ) );
      public bool IsActive
      {
         get
         {
            return (bool)GetValue( IsActiveProperty );
         }
         set
         {
            SetValue( IsActiveProperty, value );
         }
      }
      private static void OnIsActiveChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
      {
         var control = d as ClockViewControl;
         if ( control != null )
         {
            var clock = control as ClockViewControl;
            var isActive = (bool)e.NewValue;
            if( isActive )
            {
               clock.ShowClock();
            }
            else
            {
               clock.HideClock();
            }
         }
      }

      private void ShowClock()
      {
         this.Visibility = Visibility.Visible;
         Storyboard storyboard = this.TryFindResource( "ClockStoryBoard" ) as Storyboard;
         if( storyboard != null )
         {
            storyboard.Begin();
         }
      }

      private void HideClock()
      {
         this.Visibility = Visibility.Hidden;
         Storyboard storyboard = this.TryFindResource( "ClockStoryBoard" ) as Storyboard;
         if ( storyboard != null )
         {
            storyboard.Stop();
         }
      }

      private double m_scaleFactor;
   }
}
