﻿/// <summary>Definition of the interface IData.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Persistence
{
   public interface IData
   {
      string FileType { get; }

      string Version { get; }
   }
}
