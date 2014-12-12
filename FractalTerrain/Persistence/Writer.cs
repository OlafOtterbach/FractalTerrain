/// <summary>Definition of the class Writer.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
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

      public string Write( TerrainModel model )
      {
         return Serialize( _mapper.Map( model ) );
      }

      private string Serialize( DataV1 data )
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
         text += string.Format("AppleManYStartPosition {0}{1}", data.AppleManYStartPosition.ToString(CultureInfo.InvariantCulture), System.Environment.NewLine);
         return text;
      }
   }
}
