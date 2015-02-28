/// <summary>Definition of the class Writer.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.Model;
using FractalTerrain.ViewModel;
using System.Globalization;

namespace FractalTerrain.Persistence
{
   public class Writer
   {
      private ModelToDataMapper _mapper;

      public Writer()
      {
         _mapper = new ModelToDataMapper();
      }

      public string Write( TerrainModel model, ViewModelSettings settings )
      {
         return Serialize( _mapper.Map( model, settings ) );
      }

      private string Serialize( DataV1Dot1 data )
      {
         var text = string.Format("[Header]{0}", System.Environment.NewLine);
         text += string.Format("FileType {0}{1}", data.FileType, System.Environment.NewLine);
         text += string.Format("Version {0}{1}{1}", data.Version, System.Environment.NewLine);
         
         text += string.Format("[Data]{0}", System.Environment.NewLine);
         text += string.Format("MapSize {0}{1}", data.MapSize.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);
         text += string.Format("AppleManMinimalPosition {0}{1}", data.AppleManMinimalPosition.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);
         text += string.Format("AppleManMaximalPosition {0}{1}", data.AppleManMaximalPosition.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);
         text += string.Format("AppleManMinimalSize {0}{1}", data.AppleManMinimalSize.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);
         text += string.Format("AppleManSize {0}{1}", data.AppleManSize.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);
         text += string.Format("AppleManXStartPosition {0}{1}", data.AppleManXStartPosition.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);
         text += string.Format("AppleManYStartPosition {0}{1}{1}", data.AppleManYStartPosition.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);

         text += string.Format( "[Settings]{0}", System.Environment.NewLine );
         text += string.Format( "HoricontalRatio {0}{1}", data.HoricontalRatio.ToString( CultureInfo.InvariantCulture ), System.Environment.NewLine );
         text += string.Format( "VerticalRatio {0}{1}", data.VerticalRatio.ToString( CultureInfo.InvariantCulture ), System.Environment.NewLine );
         text += string.Format( "CameraTopLeft {0}{1}", data.CameraTopLeft.ToString(), System.Environment.NewLine );
         text += string.Format( "CameraTopRight {0}{1}", data.CameraTopRight.ToString(), System.Environment.NewLine );
         text += string.Format( "CameraBottomLeft {0}{1}", data.CameraBottomLeft.ToString(), System.Environment.NewLine );
         text += string.Format( "CameraBottomRight {0}{1}", data.CameraBottomRight.ToString(), System.Environment.NewLine );
         text += string.Format( "CameraSetting {0}{1}", data.CameraSetting.ToString(), System.Environment.NewLine );
         return text;
      }
   }
}
