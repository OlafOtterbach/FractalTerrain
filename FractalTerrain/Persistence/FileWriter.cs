/// <summary>Definition of the class FileWriter.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using System.IO;

namespace FractalTerrain.Persistence
{
   public class FileWriter : Writer
   {
      public Rating Write( TerrainModel model, string pathAndFileName )
      {
         var rating = new Rating();
         var text = Write( model );
         try
         {
            System.IO.File.WriteAllText( pathAndFileName, text );
         }
         catch
         {
            rating.CanNotSaveFile = true;
         }
         return rating;
      }
   }
}
