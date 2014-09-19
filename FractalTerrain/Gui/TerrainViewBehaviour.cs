using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace FractalTerrain.Gui
{
   public class TerrainViewBehaviour : Behavior<UIElement>
   {
      protected override void OnAttached()
      {
         var parent = Application.Current.MainWindow;
         var viewModel = parent.DataContext as TerrainViewModel;
         var terrainCanvas = AssociatedObject as Canvas;

         var canvas2D = new Canvas2DWpf(terrainCanvas);
         var view3D = new TerrainView3DWpf(canvas2D);
         viewModel.View = view3D;
         view3D.ViewModel = viewModel;
         terrainCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(view3D.OnMouseMove);
         terrainCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(view3D.OnMouseDown);
      }
   }
}
