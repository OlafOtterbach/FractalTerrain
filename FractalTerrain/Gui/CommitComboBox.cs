/// <summary>Definition of the class SubmitComboBox.</summary>
/// <author>Olaf Otterbach</author>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace FractalTerrain.Gui
{
   public class CommitComboBox : ComboBox
   {
      public CommitComboBox() : base()
      {
         PreviewKeyDown += new KeyEventHandler(OnPreviewKeyDown);
         this.LostFocus += new RoutedEventHandler(OnFocusLost);
      }

      protected override void OnDropDownClosed(EventArgs e)
      {
         base.OnDropDownClosed(e);
         BindingExpression bindingExpression = GetBindingExpression(ComboBox.TextProperty);
         if( bindingExpression != null )
         {
            bindingExpression.UpdateSource();
         }
      }

      public void OnFocusLost( object sender, EventArgs e)
      {
         BindingExpression bindingExpression = GetBindingExpression(ComboBox.TextProperty);
         if( bindingExpression != null )
         {
            bindingExpression.UpdateSource();
         }
      }

      void OnPreviewKeyDown(object sender, KeyEventArgs e)
      {
         if( e.Key == Key.Enter )
         {
            var bindingExpression = GetBindingExpression(ComboBox.TextProperty);
            if( bindingExpression != null )
            {
               bindingExpression.UpdateSource();
            }
         }
      }
   }
}
