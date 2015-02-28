/// <summary>Definition of the interface AppleManSettings.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.View
{
   public class AppleManSettings
   {
      public AppleManSettings()
      {}

      public AppleManSettings( AppleManSettings settings )
      {
         Map = settings.Map;
         MapSize = settings.MapSize;
         AppleManXStartPosition = settings.AppleManXStartPosition;
         AppleManYStartPosition = settings.AppleManYStartPosition;
         AppleManSize = settings.AppleManSize;
         AppleManMinimalSize = settings.AppleManMinimalSize;
         AppleManMinimalPosition = settings.AppleManMinimalPosition;
         AppleManMaximalPosition = settings.AppleManMaximalPosition;
      }

      public double[,] Map { get; set; }

      public int MapSize { get; set; }

      public double AppleManXStartPosition { get; set; }

      public double AppleManYStartPosition { get; set; }

      public double AppleManSize { get; set; }

      public double AppleManMinimalPosition { get; set; }

      public double AppleManMaximalPosition { get; set; }

      public double AppleManMinimalSize { get; set; }
   }
}
