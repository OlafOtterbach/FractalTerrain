﻿/// <summary>Definition of the class ReaderResult.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

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
