/// <summary>Definition of the class GuiCommand.</summary>
/// <author>Olaf Otterbach</author>
/// <start>15.03.2014</start>
/// <state>06.04.2014</state>

using System;
using System.Windows.Input;

namespace FractalTerrain
{
   public class GuiButtonCommand : ICommand
   {
      public GuiButtonCommand(Action action, Func<bool> canExecuteCheck)
      {
         m_action = action;
         m_canExecuteCheck = canExecuteCheck;
      }

      public event EventHandler CanExecuteChanged
      {
         add { CommandManager.RequerySuggested += value; }
         remove { CommandManager.RequerySuggested -= value; }
      }

      public bool CanExecute(object parameter)
      {
         return  m_canExecuteCheck();
      }

      public void Execute(object parameter)
      {
         m_action();
      }

      private Action m_action;

      private Func<bool> m_canExecuteCheck;
   }
}
