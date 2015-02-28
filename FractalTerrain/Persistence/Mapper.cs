/// <summary>Definition of the class Mapper.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System.Globalization;

namespace FractalTerrain.Persistence
{
   public abstract class Mapper
   {
      private Mapper _succesor;
      private Converter _converter;

      public Mapper( Mapper succesor, Converter converter )
      {
         _succesor = succesor;
         _converter = converter;
      }

      public abstract bool CanHandle( Context ctx );

      public Context Map( Context ctx )
      {
         if( ctx.Rating.HasParseError )
         {
            ctx.Rating.HasMappingError = true;
            return ctx;
         }
         return CanHandle( ctx ) ? MapToData( ctx ) : _converter.Convert( _succesor.Map( ctx ) );
      }

      protected abstract Context MapToData(Context ctx);

      protected double TryParseDouble( string text )
      {
         double res = 0.0;
         if( double.TryParse( text, NumberStyles.Number, CultureInfo.InvariantCulture, out res ) )
         {
            return res;
         }
         else
         {
            return 0.0;
         }
      }

      protected int ParseInt( string text )
      {
         int result = 0;
         int.TryParse( text, NumberStyles.Number, CultureInfo.InvariantCulture, out result );
         return result;
      }

      protected double ParseDouble( string text )
      {
         double result = 0.0;
         double.TryParse( text, NumberStyles.Number, CultureInfo.InvariantCulture, out result );
         return result;
      }
   }
}
