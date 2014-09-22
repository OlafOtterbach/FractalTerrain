/// <summary>Definition of the class TerrainViewModel.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FractalTerrain.ViewModel
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

      public bool ParallelProcess { get; set; }


      public int GridSize
      {
         get
         {
            return m_model.MapSize;
         }
         set
         {
            var save = value;
            m_model.MapSize = save;
            Update(ParallelProcess);
            OnPropertyChanged("GridSize");
         }
      }


      public double AppleManSize
      {
         get
         {
            var size = m_model.AppleManSize;
            var maximalSize = ( m_model.MaximalStartPosition + 1.0) - m_model.MinimalStartPosition;
            var scrollBarSize = ( ( size / maximalSize ) * 100.0 );
            return scrollBarSize;
         }
         set
         {
            var maximalSize = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var size = value;
            var newSize = ( size / 100.0 ) * maximalSize;
            m_model.AppleManSize = newSize;
            Update(ParallelProcess);
            OnPropertyChanged("AppleManSize");
         }
      }


      public double AppleManXStartPosition
      {
         get
         {
            var startX = m_model.AppleManXStartPosition - m_model.MinimalStartPosition;
            var size = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var scrollBarSize = ( ( startX / size ) * 100.0 );
            return scrollBarSize;
         }
         set
         {
            var size = ( m_model.MaximalStartPosition + 1.0 ) - m_model.MinimalStartPosition;
            var startX = value;
            var newPos = ( startX / 100.0 ) * size + m_model.MinimalStartPosition;
            m_model.AppleManXStartPosition = newPos;
            Update(ParallelProcess);
            OnPropertyChanged("AppleManXStartPosition");
         }
      }


      public double AppleManYStartPosition
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
            Update(ParallelProcess);
            OnPropertyChanged("AppleManYStartPosition");
         }
      }



      public void Resize()
      {
         View.Resize();
         View.Render();
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
         Update( true );
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



      private void Update( bool renderTerrain )
      {
         m_model.Update();
         AppleView.Update();
         AppleView.Render();
         if( renderTerrain )
         {
            View.Update();
            View.Render();
         }
      }


/*
      private void UpdateModel()
      {
         m_model.Update();
      }


      private void Update( bool renderTerrain )
      {
         AppleView.Update();
         AppleView.Render();
         if( renderTerrain )
         {
            View.Update();
            View.Render();
         }
      }
*/

      private void CreateTerrainModel()
      {
         var mapSize = 129;
         var appleSize = 0.31665934879948815;
         var appleXPos = -1.4208065326410015;
         var appleYPos = -0.0036470011473482278;
         var terrainBuilder = new TerrainModelBuilder();
         m_model = terrainBuilder.Create(mapSize, appleXPos, appleYPos, appleSize);
      }

      private TerrainModel m_model;
   }
}
