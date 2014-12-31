/// <summary>Definition of the class ModelValidator.</summary>
/// <author>Olaf Otterbach</author>

using System.Globalization;

namespace FractalTerrain.Persistence
{
   public class ModelValidator
   {
      public Context Validate( Context ctx )
      {
         var model = ctx.Model;
         if ( model == null )
         {
            ctx.Rating.NoModelToValidate = true;
            return ctx;
         }
         if ( model.MapSize <= 0 )
         {
            ctx.Rating.InvalidMapSize = true;
         }
         if ( model.AppleManSize <= 0.0 )
         {
            ctx.Rating.InvalidAppleManSize = true;
         }
         return ctx;
      }
   }
}
