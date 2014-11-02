/// <summary>Definition of the class TerrainViewModel.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FractalTerrain.ViewModel
{
   public class TerrainViewModel : INotifyPropertyChanged
   {
      public TerrainViewModel()
      {
         mTerrainViews = new List<TerrainView3D>();
         mAppleViews = new List<AppleManViewWpf>();
         CreateTerrainModel();
         Start = new GuiButtonCommand(() => OnStart(), () => OnStartCanBeExecuted());
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public ICommand Start { get; private set; }

      public IEnumerable<TerrainView3D> TerrainViews { get { return mTerrainViews; } }

      private List<TerrainView3D> mTerrainViews;

      public void Register(TerrainView3D view)
      {
         mTerrainViews.Add(view);
      }

      private List<AppleManViewWpf> AppleViews { get { return mAppleViews; } }

      private List<AppleManViewWpf> mAppleViews;

      public void Register(AppleManViewWpf view)
      {
         mAppleViews.Add(view);
      }


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
            OnPropertyChanged("GridSize");
            Update(ParallelProcess);
         }
      }


      public double AppleManMaximumSize
      {
         get
         {
            return m_model.AppleManMaximalPosition + m_model.AppleManMinimalSize - m_model.AppleManMinimalPosition;
         }
      }

      public double AppleManMinimum
      {
         get
         {
            return m_model.AppleManMinimalPosition;
         }
      }

      public double AppleManMaximum
      {
         get
         {
            return m_model.AppleManMaximalPosition;
         }
      }

      public double AppleManSize
      {
         get
         {
            return m_model.AppleManSize;
         }
         set
         {
            m_model.AppleManSize = value;
            Update(ParallelProcess);
         }
      }


      public double AppleManXStartPosition
      {
         get
         {
            return m_model.AppleManXStartPosition;
         }
         set
         {
            m_model.AppleManXStartPosition = value;
            Update(ParallelProcess);
         }
      }


      public double AppleManYStartPosition
      {
         get
         {
            return m_model.AppleManYStartPosition;
         }
         set
         {
            m_model.AppleManYStartPosition = value;
            Update(ParallelProcess);
         }
      }



      public void Resize()
      {
         mTerrainViews.ForEach(v => { v.Resize(); });
         mTerrainViews.ForEach(v => { v.Render(); });
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
         mAppleViews.ForEach(v => { v.Update(); });
         mAppleViews.ForEach(v => { v.Render(); });
         if( renderTerrain )
         {
            mTerrainViews.ForEach(v => { v.Update(); });
            mTerrainViews.ForEach(v => { v.Render(); });
         }
         OnPropertyChanged("GridSize");
         OnPropertyChanged("AppleManXStartPosition");
         OnPropertyChanged("AppleManYStartPosition");
         OnPropertyChanged("AppleManSize");
         OnPropertyChanged("AppleManMaximumSize");
         OnPropertyChanged("AppleManMinimum");
         OnPropertyChanged("AppleManMaximum");
      }


      private void RenderTerrain()
      {
         mTerrainViews.ForEach(v => { v.Render(); });
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

      private TerrainModel m_model;
   }
}
