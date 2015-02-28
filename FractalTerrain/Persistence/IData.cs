/// <summary>Definition of the interface IData.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.Persistence
{
   public interface IData
   {
      string FileType { get; }

      string Version { get; }
   }
}
