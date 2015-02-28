/// <summary>Definition of the class CommitComboBox.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

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
         UpdateTextPropertyBinding();
      }

      public void OnFocusLost( object sender, EventArgs e)
      {
         UpdateTextPropertyBinding();
      }

      private void OnPreviewKeyDown(object sender, KeyEventArgs e)
      {
         if( e.Key == Key.Enter )
         {
            UpdateTextPropertyBinding();
         }
      }

      private void UpdateTextPropertyBinding()
      {
         BindingExpression bindingExpression = GetBindingExpression(ComboBox.TextProperty);
         if( bindingExpression != null )
         {
            bindingExpression.UpdateSource();
         }
      }
   }
}
