/// <summary>Definition of the class Reader.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Persistence
{
   public class Reader
   {
      private Parser _parser;
      private DataToModelMapper _mapper;

      public Reader()
      {
         _parser = new Parser();
         _mapper = new DataToModelMapper( new MapperV1( new MapperUnknown(), new NullConverter() ) );
      }

      public ReaderResult Read( string text )
      {
         var context = new Context() { Text = text };
         context = _mapper.Map( _parser.Parse( context ) );
         var result = new ReaderResult
         {
            Model = context.Model,
            Rating = context.Rating
         };
         return result;
      }
   }
}
