﻿/// <summary>Definition of the class TerrainViewModel.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.Persistence;
using FractalTerrain.View;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using System.Windows;

namespace FractalTerrain.ViewModel
{
   public class TerrainViewModel : INotifyPropertyChanged
   {
      public TerrainViewModel()
      {
         Camera1 = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         Camera2 = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         Camera3 = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         Camera4 = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         Camera5 = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         ColumnRatio = new GridLength(1, GridUnitType.Star );
         RowRatio = new GridLength( 1, GridUnitType.Star );

         mTerrainViews = new List<TerrainView3D>();
         mAppleViews = new List<AppleManViewWpf>();
         VisualModel = new VisualModel();

         CreateTerrainModel();
         CommandStart = new GuiButtonCommand(() => OnStart(), () => OnStartCanBeExecuted());
         CommandNew = new GuiButtonCommand(() => OnNew(), () => OnNewCanBeExecuted());
         CommandOpen = new GuiButtonCommand(() => OnOpen(), () => OnOpenCanBeExecuted());
         CommandSave = new GuiButtonCommand(() => OnSave(), () => OnSaveCanBeExecuted());
         CommandSaveAs = new GuiButtonCommand(() => OnSaveAs(), () => OnSaveAsCanBeExecuted());
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public ICommand CommandStart { get; private set; }
      public ICommand CommandNew{ get; private set; }
      public ICommand CommandOpen { get; private set; }
      public ICommand CommandSave { get; private set; }
      public ICommand CommandSaveAs { get; private set; }

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

      public CameraSettings Camera1 { get; set; }
      public CameraSettings Camera2 { get; set; }
      public CameraSettings Camera3 { get; set; }
      public CameraSettings Camera4 { get; set; }
      public CameraSettings Camera5 { get; set; }
      public GridLength ColumnRatio { get; set; }
      public GridLength RowRatio { get; set; }

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

      public VisualModel VisualModel { get; private set; }

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

      public bool OnNewCanBeExecuted()
      {
         return true;
      }

      public void OnNew()
      {
         var a = 5;
         a++;
      }

      public bool OnOpenCanBeExecuted()
      {
         return true;
      }

      public void OnOpen()
      {
         OpenFileDialog fileDialog = new OpenFileDialog();
         fileDialog.Filter = @"Fractal files (*.frac)|*.frac";
         fileDialog.InitialDirectory = Environment.CurrentDirectory;
         if ( fileDialog.ShowDialog() == true )
         {
            var pathAndFile = fileDialog.FileName;
            var reader = new FileReader();
            var result = reader.Read( pathAndFile );
            m_model = result.Model;
            Update( true );
         }
      }

      private void Message( Rating rating )
      {
         //var messageBox = new Message
      }

      public bool OnSaveCanBeExecuted()
      {
         return true;
      }

      public void OnSave()
      {
         var camera1 = Camera1;
         var row = RowRatio.Value;
         var column = ColumnRatio.Value;
         var writer = new FileWriter();
         writer.Write( m_model, @"c:\tmp\test.frac" );
      }

      public bool OnSaveAsCanBeExecuted()
      {
         return true;
      }

      public void OnSaveAs()
      {
         var writer = new FileWriter();
         writer.Write( m_model, @"c:\tmp\test.frac" );
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
         VisualModel.InitTerrain(m_model);
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
