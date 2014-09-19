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
   /// Interaction logic for ImageViewControl.xaml
   /// </summary>
   public partial class ImageViewControl : UserControl
   {
      public ImageViewControl()
      {
         InitializeComponent();
      }

      public static readonly DependencyProperty m_textProperty = DependencyProperty.Register("Text", 
         typeof(string), typeof(ImageViewControl), 
         new FrameworkPropertyMetadata("Hallo", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,OnPropertyChanged ) );
      public string Text
      {
         get 
         { 
            return (string)GetValue(m_textProperty); 
         }
         set 
         { 
            SetValue(m_textProperty, value);
         }
      }

      private static void OnPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e)
      {
         var control = d as ImageViewControl;
         if( control != null )
         {
            control.ControlTextBox.Text = control.Text;
         }
      }
   }
}
