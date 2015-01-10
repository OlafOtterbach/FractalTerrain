/// <summary>Definition of the class ReaderResult.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.ViewModel;

namespace FractalTerrain.Persistence
{
   public class ReaderResult
   {
      public ReaderResult()
      {
         Rating = new Rating() { ResultIsEmpty = true };
      }
      public Rating Rating { get; set; }

      public TerrainModel Model { get; set; }

      public ViewModelSettings Settings { get; set; }
   }
}
