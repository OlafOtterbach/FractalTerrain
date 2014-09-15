/// <summary>Definition of the class SubmitComboBox.</summary>
/// <author>Olaf Otterbach</author>

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FractalTerrain.Gui
{
   public class SubmitComboBox : ComboBox
   {
      public SubmitComboBox() : base()
      {
         PreviewKeyDown += new KeyEventHandler(SubmitTextBox_PreviewKeyDown);
      }

      void SubmitTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
      {
         if( e.Key == Key.Enter )
         {
            BindingExpression be = GetBindingExpression(ComboBox.TextProperty);
            if( be != null )
            {
               be.UpdateSource();
            }
         }
      }
   }
}
