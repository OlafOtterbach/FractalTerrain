using System.Text.RegularExpressions;
namespace FractalTerrain.ViewModel
{
   public class CameraSettings
   {
      public CameraSettings()
      {
         AngleAxisEz = 0.0;
         AngleAxisEy = 0.0;
         Distance = 0.0;
      }

      public CameraSettings( string text )
      {
         AngleAxisEz = 0.0;
         AngleAxisEy = 0.0;
         Distance = 0.0;
         var pattern = @"^(?:\()(?<DoubleValue>[^\]]*)(?:\])(?:[\s\r\n]+)((?<Key>[^\s\[]+)(?:[^\S\[\r\n]+)(?<Value>[^\s\[]+)(?:[\r\n]+))*(?:[\r\n\s]*)";
         try
         {
            var matches = Regex.Matches( text, pattern, RegexOptions.Singleline );

         }
      }

      public double AngleAxisEz { get; set; }

      public double AngleAxisEy { get; set; }

      public double Distance { get; set; }

      public static string ToString( this CameraSettings settings )
      {
         var res = string.Format( "({0},{1},{2})", settings.AngleAxisEz, settings.AngleAxisEy, settings.Distance );
         return res;
      }
   }
}
