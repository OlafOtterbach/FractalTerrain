﻿/// <summary>Definition of the class MapperUnknown.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

namespace FractalTerrain.Persistence
{
   public class MapperUnknown : Mapper
   {
      public MapperUnknown()
         : base(null, null)
      { }

      public override bool CanHandle(Context ctx)
      {
         return true;
      }

      protected override Context MapToData(Context ctx)
      {
         ctx.Rating.HasUnkownVersion = true;
         return ctx;
      }
   }
}
