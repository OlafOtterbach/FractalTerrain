using System.Globalization;
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

      public CameraSettings( CameraSettings setting )
      {
         AngleAxisEz = setting.AngleAxisEz;
         AngleAxisEy = setting.AngleAxisEy;
         Distance = setting.Distance;
      }


      public double AngleAxisEz { get; set; }

      public double AngleAxisEy { get; set; }

      public double Distance { get; set; }


      public override string ToString()
      {
         var res = string.Format( "({0},{1},{2})", AngleAxisEz.ToString( CultureInfo.InvariantCulture ), 
                                                   AngleAxisEy.ToString( CultureInfo.InvariantCulture ), 
                                                   Distance.ToString( CultureInfo.InvariantCulture )     );
         return res;
      }

      public static CameraSettings TryParse( string text )
      {
         CameraSettings settings = null;
         var pattern = @"^(?:\()(?:(\s)*)(?<FirstAngle>(\d)*(.?)(\d)+)(?:(\s)*)(?:,)(?:(\s)*)(?<SecondAngle>(\d)*(.?)(\d)+)(?:(\s)*)(?:,)(?:(\s)*)(?<Distance>(\d)*(.?)(\d)+)(?:(\s)*)(?:\))";
         var match = Regex.Match( text, pattern, RegexOptions.Singleline );
         if ( match.Success )
         {
            try
            {
               settings = new CameraSettings
               {
                  AngleAxisEz = ParseDouble( match.Groups["FirstAngle"].Value ),
                  AngleAxisEy = ParseDouble( match.Groups["SecondAngle"].Value ),
                  Distance = ParseDouble( match.Groups["Distance"].Value )
               };
            }
            catch
            {
               settings = new CameraSettings();
            }
         }
         return settings;
      }

      private static double ParseDouble( string text )
      {
         double result = 0.0;
         double.TryParse( text, NumberStyles.Number, CultureInfo.InvariantCulture, out result );
         return result;
      }
   }
}
