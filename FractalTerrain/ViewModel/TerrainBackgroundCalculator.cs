/// <summary>Definition of the class TerrainBackgroundCalculator.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.Model;
using FractalTerrain.View;
using System;
using System.ComponentModel;

namespace FractalTerrain.ViewModel
{
   internal class TerrainBackgroundCalculator
   {
      public TerrainBackgroundCalculator()
      {
         m_backWorker = new BackgroundWorker();
         m_backWorker.DoWork += new DoWorkEventHandler(DoCalculate);
         m_backWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackCompleted);
      }

      public void Calculate( TerrainModel model, Action<VisualTerrainModel> completedAction )
      {
         if (!m_backWorker.IsBusy)
         {
            m_model = model;
            m_completedAction = completedAction;
            m_backWorker.RunWorkerAsync();
         }
      }

      public bool IsBusy
      {
         get
         {
            return m_backWorker.IsBusy;
         }
      }

      private void DoCalculate(object sender, DoWorkEventArgs e)
      {
         m_model.Update();
         m_visualModel = TerrainToVisualModelConverter.Convert(m_model);
      }

      private void BackCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         m_completedAction(m_visualModel);
      }

      Action<VisualTerrainModel> m_completedAction;

      private BackgroundWorker m_backWorker;

      TerrainModel m_model;

      VisualTerrainModel m_visualModel;
   }
}
