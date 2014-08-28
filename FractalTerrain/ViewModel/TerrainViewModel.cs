/// <summary>Definition of the class TerrainViewModel.</summary>
/// <author>Olaf Otterbach</author>

using System.ComponentModel;
using System.Windows.Input;

namespace FractalTerrain
{
   public class TerrainViewModel : INotifyPropertyChanged
   {
      public TerrainViewModel()
      {
         CreateTerrainModel();
         Start = new GuiButtonCommand(() => OnStart(), () => OnStartCanBeExecuted());
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public ICommand Start { get; private set; }

      public TerrainView3D View { get; set; }

      public AppleManViewWpf AppleView { get; set; }


      public int AppleManSize
      {
         get
         {
            var size = m_model.AppleManSize;
            var maximalSize = ( m_model.MaximalStartPosition + 1.0) - m_model.MinimalStartPosition;
            var scrollBarSize = (int)( ( size / maximalSize ) * 100.0 );
            return scrollBarSize;
         }
         set
         {
            var maximalSize = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var size = (double)value;
            var newSize = ( size / 100.0 ) * maximalSize;
            m_model.AppleManSize = newSize;
            m_model.UpdateAppleMan();
            AppleView.Update();
            AppleView.Render();
         }
      }

      public int AppleManXStartPosition
      {
         get
         {
            var startX = m_model.AppleManXStartPosition - m_model.MinimalStartPosition;
            var size = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var scrollBarSize = (int)( ( startX / size ) * 100.0 );
            return scrollBarSize;
         }
         set
         {
            var size = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var startX = (double)value;
            var newPos = ( startX / 100.0 ) * size + m_model.MinimalStartPosition;
            m_model.AppleManXStartPosition = newPos;
            m_model.UpdateAppleMan();
            AppleView.Update();
            AppleView.Render();
         }
      }

      public int AppleManYStartPosition
      {
         get
         {
            var startY = m_model.AppleManYStartPosition - m_model.MinimalStartPosition;
            var size = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var scrollBarSize = (int)( ( startY / size ) * 100.0 );
            return scrollBarSize;
         }
         set
         {
            var size = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var startY = (double)value;
            var newPos = ( startY / 100.0 ) * size + m_model.MinimalStartPosition;
            m_model.AppleManYStartPosition = newPos;
            m_model.UpdateAppleMan();
            AppleView.Update();
            AppleView.Render();
         }
      }



      public void Resize()
      {
         View.Resize();
      }

      public TerrainModel ActualModel
      {
         get
         {
            return m_model;
         }
      }
      
      public void OnStart()
      {
         UpdateTerrainModel();
         UpdateView();
         OnPropertyChanged("Size");
         OnPropertyChanged("Exemplares");
         OnPropertyChanged("ActualModelIndex");
      }

      public bool OnStartCanBeExecuted()
      {
         return true;
      }

      // Create the OnPropertyChanged method to raise the event
      protected void OnPropertyChanged(string name)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null)
         {
            handler(this, new PropertyChangedEventArgs(name));
         }
      }

      private void CreateTerrainModel()
      {
         var mapSize = 129;
         var appleSize = 0.31665934879948815;
         var appleXPos = -1.4208065326410015;
         var appleYPos = -0.0036470011473482278;
         var terrainBuilder = new TerrainModelBuilder();
         m_model = terrainBuilder.Create(mapSize, appleXPos, appleYPos, appleSize);
      }

      private void UpdateTerrainModel()
      {
         m_model.Update();
      }

      private void UpdateView()
      {
         AppleView.Update();
         View.Update();
         AppleView.Render();
         View.Render();
      }

      private TerrainModel m_model;
   }
}
