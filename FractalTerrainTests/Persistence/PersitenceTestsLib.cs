/// <summary>Definition of the class PersitenceTestsLib.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using FractalTerrain.ViewModel;
using System;
namespace FractalTerrainTests.Persistence
{
   public static class PersitenceTestsLib
   {
      public static Tuple<TerrainModel, ViewModelSettings> GetModelAndSettings()
      {
         var model = new TerrainModel(null, null)
         {
            MapSize = 9,
            AppleManMinimalPosition = -5.0,
            AppleManMaximalPosition = 5.0,
            AppleManMinimalSize = 0.1,
            AppleManSize = 4.0,
            AppleManXStartPosition = -2.0,
            AppleManYStartPosition = -2.0,
         };

         var settings = new ViewModelSettings
         {
            HoricontalRatio = 0.5,
            VerticalRatio = 0.6,
            CameraBottomLeft = new CameraSettings { AngleAxisEz = 0.0, AngleAxisEy = 1.0, Distance = 2.0 },
            CameraBottomRight = new CameraSettings { AngleAxisEz = 3.0, AngleAxisEy = 4.0, Distance = 5.0 },
            CameraTopLeft = new CameraSettings { AngleAxisEz = 6.0, AngleAxisEy = 7.0, Distance = 8.0 },
            CameraTopRight = new CameraSettings { AngleAxisEz = 9.0, AngleAxisEy = 10.0, Distance = 11.0 },
            CameraSetting = new CameraSettings { AngleAxisEz = 12.0, AngleAxisEy = 13.0, Distance = 14.0 },
         };

         return new Tuple<TerrainModel, ViewModelSettings>(model, settings);
      }
   }
}
