/// <summary>Definition of the class Context.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using FractalTerrain.Model;
using FractalTerrain.ViewModel;

namespace FractalTerrain.Persistence
{
   public class Context
   {
      public Context()
      {
         Rating = new Rating();
      }

      public string Text { get; set;}

      public Rating Rating { get; set; }

      public ParserData ParserData { get; set; }

      public IData Data { get; set; }

      public TerrainModel Model { get; set; }

      public ViewModelSettings Setting { get; set; }
   }
}
