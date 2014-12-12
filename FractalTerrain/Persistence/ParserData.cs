/// <summary>Definition of the class ParserData.</summary>
/// <author>Olaf Otterbach</author>

using System.Collections.Generic;

namespace FractalTerrain.Persistence
{
   public class ParserData : IData
   {
      public string FileType { get; set; }
      public string Version { get; set; }
      public Dictionary<string, Dictionary<string,string>> Data { get; set; }
   }
}
