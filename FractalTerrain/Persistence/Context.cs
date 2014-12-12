﻿/// <summary>Definition of the class Context.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;

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
   }
}
