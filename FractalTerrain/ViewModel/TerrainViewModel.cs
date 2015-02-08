/// <summary>Definition of the class TerrainViewModel.</summary>
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
using System.IO;

namespace FractalTerrain.ViewModel
{
   public class TerrainViewModel : INotifyPropertyChanged
   {
      public TerrainViewModel()
      {
         m_backWorker = new TerrainBackgroundCalculator();

         FileName = "";

         CameraTopLeft = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         CameraTopRight = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         CameraBottomLeft = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         CameraBottomRight = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         CameraSetting = new CameraSettings { AngleAxisEz = 45.0, AngleAxisEy = 25.0, Distance = 150.0 };
         ColumnRatio = new GridLength(1, GridUnitType.Star );
         RowRatio = new GridLength( 1, GridUnitType.Star );

         CreateTerrainModel();
         CommandStart = new GuiButtonCommand(() => OnStart(), () => OnStartCanBeExecuted());
         CommandNew = new GuiButtonCommand(() => OnNew(), () => OnNewCanBeExecuted());
         CommandOpen = new GuiButtonCommand(() => OnOpen(), () => OnOpenCanBeExecuted());
         CommandSave = new GuiButtonCommand(() => OnSave(), () => OnSaveCanBeExecuted());
         CommandSaveAs = new GuiButtonCommand(() => OnSaveAs(), () => OnSaveAsCanBeExecuted());

         ActiveClock = false;
      }

      public event PropertyChangedEventHandler PropertyChanged;

      public ICommand CommandStart { get; private set; }
      public ICommand CommandNew{ get; private set; }
      public ICommand CommandOpen { get; private set; }
      public ICommand CommandSave { get; private set; }
      public ICommand CommandSaveAs { get; private set; }

      public bool ActiveClock 
      { 
         get
         {
            return m_ActiveClock;
         }
         set
         {
            m_ActiveClock = value;
            OnPropertyChanged( "ActiveClock" );
         }
      }
      private bool m_ActiveClock;

      public bool ParallelProcess { get; set; }

      public CameraSettings CameraTopLeft { get; set; }
      public CameraSettings CameraTopRight { get; set; }
      public CameraSettings CameraBottomLeft { get; set; }
      public CameraSettings CameraBottomRight { get; set; }
      public CameraSettings CameraSetting { get; set; }
      public double WidthLeft { get; set; }
      public double WidthRight { get; set; }
      public double HeightTop { get; set; }
      public double HeightBottom { get; set; }
      public GridLength ColumnRatio { get; set; }
      public GridLength RowRatio { get; set; }

      private string m_fileName;
      public string FileName
      {
         get
         {
            return m_fileName;
         }
         set
         {
            m_fileName = value;
            OnPropertyChanged( "Title" );
         }
      }

      public string Title
      {
         get
         {
            var fullTitle = (m_fileName == string.Empty ) ? "FractalTerrain" : "FractalTerrain - " + m_fileName;
            return fullTitle;
         }
      }


      private bool m_isExpanded;
      public bool IsExpanded 
      {
         get
         {
            return m_isExpanded;
         }
         set
         {
            m_isExpanded = value;
            OnPropertyChanged( "IsExpanded" );
         }
      }

      public int GridSize
      {
         get
         {
            return m_model.MapSize;
         }
         set
         {
            m_model.MapSize = value;
            OnPropertyChanged("GridSize");
            Update(ParallelProcess);
         }
      }

      public AppleManSettings AppleManSettings
      {
         get
         {
            var settings = new AppleManSettings
            {
               Map = m_model.AppleManData.Map,
               MapSize = m_model.AppleManData.Size,
               AppleManXStartPosition = m_model.AppleManXStartPosition,
               AppleManYStartPosition = m_model.AppleManYStartPosition,
               AppleManSize = m_model.AppleManSize,
               AppleManMinimalSize = m_model.AppleManMinimalSize,
               AppleManMinimalPosition = m_model.AppleManMinimalPosition,
               AppleManMaximalPosition = m_model.AppleManMaximalPosition
            };
            return settings;
         }
         set
         {
            var settings = value;
            m_model.AppleManXStartPosition = settings.AppleManXStartPosition;
            m_model.AppleManYStartPosition = settings.AppleManYStartPosition;
            m_model.AppleManSize = settings.AppleManSize;
            m_model.AppleManMinimalSize = settings.AppleManMinimalSize;
            m_model.AppleManMinimalPosition = settings.AppleManMinimalPosition;
            m_model.AppleManMaximalPosition = settings.AppleManMaximalPosition;
            Update(ParallelProcess);
            OnPropertyChanged("AppleManSettings");
         }
      }

      private VisualTerrainModel m_visualTerrainModel;
      public VisualTerrainModel VisualTerrainModel
      { 
         get
         {
            return m_visualTerrainModel;
         }
         set
         {
            m_visualTerrainModel = value;
            OnPropertyChanged("VisualTerrainModel");
         }
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
            if ( result.Rating.AllSatisfied )
            {
               m_model = result.Model;
               var mapper = new ViewModelAndSettingsMapper();
               mapper.MapSettingsToViewModel( result.Settings, this );
               FileName = fileDialog.FileName;
               IsExpanded = false;
               Update( true );
            }
            else
            {
               Message( result.Rating );
            }
         }
      }

      private void Message( Rating rating )
      {
         if( !rating.AllSatisfied)
         {
            var message = "";
            if ( rating.CanNotOpenFile ) message = "Can not open file.";
            else if ( rating.HasParseError ) message = "File is not a valid frac file.";
            else if ( rating.HasUnkownVersion ) message = "Unknown file version.";
            else if ( rating.HasMappingError ) message = "File is not loadable.";
            if ( message != string.Empty )
            {
               MessageBoxResult result = MessageBox.Show( message );
            }
         }
      }

      public bool OnSaveCanBeExecuted()
      {
         return ( FileName != string.Empty );
      }

      public void OnSave()
      {
         var writer = new FileWriter();
         var mapper = new ViewModelAndSettingsMapper();
         writer.Write( m_model, mapper.CreateSettingsFromViewModel( this ), FileName );
      }



      public bool OnSaveAsCanBeExecuted()
      {
         return true;
      }

      public void OnSaveAs()
      {
         SaveFileDialog saveFileDialog = new SaveFileDialog();
         saveFileDialog.Filter = "frac files (*.frac)|*.frac|All files (*.*)|*.*";
         saveFileDialog.FilterIndex = 2;
         saveFileDialog.RestoreDirectory = true;
         if ( saveFileDialog.ShowDialog() == true )
         {
            var fileName = saveFileDialog.FileName;
            var writer = new FileWriter();
            var mapper = new ViewModelAndSettingsMapper();
            writer.Write( m_model, mapper.CreateSettingsFromViewModel( this ), fileName );
            FileName = fileName;
            IsExpanded = false;
         }

      }


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
         if ( renderTerrain )
         {
            if ( !m_backWorker.IsBusy )
            {
               ActiveClock = true;
               m_backWorker.Calculate(m_model, (VisualTerrainModel visualModel) =>
               {
                  ActiveClock = false;
                  VisualTerrainModel = visualModel;
                  PropertiesChanged();
               });
            }
         }
         else
         {
            m_model.UpdateAppleMan();
            PropertiesChanged();
         }
      }


      private void PropertiesChanged()
      {
         OnPropertyChanged("VisualTerrainModel");
         OnPropertyChanged("AppleManSettings");
         OnPropertyChanged( "GridSize" );
         OnPropertyChanged( "CameraTopLeft" );
         OnPropertyChanged( "CameraTopRight" );
         OnPropertyChanged( "CameraBottomLeft" );
         OnPropertyChanged( "CameraBottomRight" );
         OnPropertyChanged( "CameraSetting" );
         OnPropertyChanged( "RowRatio" );
         OnPropertyChanged( "ColumnRatio" );
         OnPropertyChanged( "WidthLeft" );
         OnPropertyChanged( "WidthRight" );
         OnPropertyChanged( "HeightTop" );
         OnPropertyChanged( "HeightBottom" );
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

      private TerrainBackgroundCalculator m_backWorker;
   }
}
