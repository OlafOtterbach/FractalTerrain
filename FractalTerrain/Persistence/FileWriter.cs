/// <summary>Definition of the class FileWriter.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.ViewModel;
using System.IO;

namespace FractalTerrain.Persistence
{
   public class FileWriter : Writer
   {
      public Rating Write( TerrainModel model, ViewModelSettings settings, string pathAndFileName )
      {
         var rating = new Rating();
         var text = Write( model, settings );
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
