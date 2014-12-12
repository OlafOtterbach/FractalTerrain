/// <summary>Definition of the class DataV1.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Persistence
{
   public class DataV1 : IData
   {
      public DataV1()
      {
         FileType = "FractalTerrain";
         Version = "V1.0";
      }

      public string FileType { get; private set; }

      public string Version { get; private set; }

      public int MapSize { get; set; }

      public double AppleManXStartPosition { get; set; }

      public double AppleManYStartPosition { get; set; }

      public double AppleManSize { get; set; }

      public double AppleManMinimalPosition { get; set; }

      public double AppleManMaximalPosition { get; set; }

      public double AppleManMinimalSize { get; set; }
   }
}
