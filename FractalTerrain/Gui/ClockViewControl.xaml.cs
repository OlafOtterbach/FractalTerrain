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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FractalTerrain.Gui
{
   /// <summary>
   /// Interaction logic for ClockViewControl.xaml
   /// </summary>
   public partial class ClockViewControl : UserControl
   {
      public ClockViewControl()
      {
         InitializeComponent();
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
   }
}
