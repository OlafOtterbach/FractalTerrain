/// <summary>Definition of the class MinimumConverter.</summary>
/// <author>Olaf Otterbach</author>

using System;
using System.Linq;
using System.Windows.Data;

namespace FractalTerrain.Gui
{
   public class MinimumConverter : IMultiValueConverter
   {
      public object Convert(object[] values, Type targetType, object parameter,
          System.Globalization.CultureInfo culture)
      {
         var minimum = values.Min();
         return minimum;
      }

      public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
          System.Globalization.CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}