using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace FractalTerrain
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         this.InitializeComponent();
//         var appleCanvas = FindName("AppleCanvas") as Canvas;
//         var appleManView = new AppleManViewWpf(appleCanvas);
         var appleImage = FindName("AppleImage") as Image;
         var appleManView = new AppleManViewWpf(appleImage);
         var canvas2D = new Canvas2DWpf(this.canvas);
         var view3D = new TerrainView3DWpf(canvas2D);
         var viewModel = new TerrainViewModel() { View = view3D, AppleView = appleManView };
         appleManView.ViewModel = viewModel;
         view3D.ViewModel = viewModel;
         this.DataContext = viewModel;
         this.canvas.MouseMove += new System.Windows.Input.MouseEventHandler(view3D.OnMouseMove);
         this.canvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(view3D.OnMouseDown);
         this.SizeChanged += new SizeChangedEventHandler(this.OnResize);
      }

      private void OnContentRendered(object sender, EventArgs e)
      {
         var viewModel = DataContext as TerrainViewModel;
         viewModel.OnStart();
      }


      /// <summary>
      /// Callback for resize event.
      /// </summary>
      /// <param name="t_sender">sender of event</param>
      /// <param name="t_event">event that is sent</param>
      private void OnResize(object t_sender, RoutedEventArgs t_event)
      {
         var viewModel = DataContext as TerrainViewModel;
         viewModel.Resize();
      }

   }
}
