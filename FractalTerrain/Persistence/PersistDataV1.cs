namespace FractalTerrain.Persistence
{
   public class PersistDataV1
   {
      public PersistDataV1()
      {
         Version = "FractalTerrain V1.0";
      }

      public string Version { get; private set; }

      public int MapSize { get; set; }

      public double[] TerrainMap { get; set; }

      public double[] AppleManMap { get; set; }

      public double AppleManXStartPosition { get; set; }

      public double AppleManYStartPosition { get; set; }

      public double AppleManSize { get; set; }

      public double AppleManMinimalPosition { get; set; }

      public double AppleManMaximalPosition { get; set; }

      public double AppleManMinimalSize { get; set; }
   }
}
