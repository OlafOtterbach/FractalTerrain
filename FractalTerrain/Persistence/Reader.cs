﻿/// <summary>Definition of the class Reader.</summary>
/// <author>Olaf Otterbach</author>

namespace FractalTerrain.Persistence
{
   public class Reader
   {
      private Parser _parser;
      private DataToModelMapper _mapper;
      private ModelValidator _validator;

      public Reader()
      {
         _parser = new Parser();
         _mapper = new DataToModelMapper( new MapperV1( new MapperUnknown(), new NullConverter() ) );
         _validator = new ModelValidator();
      }

      public ReaderResult Read( string text )
      {
         var context = new Context() { Text = text };
         context = _validator.Validate( _mapper.Map( _parser.Parse( context ) ) );
         var result = new ReaderResult
         {
            Model = context.Model,
            Rating = context.Rating
         };
         return result;
      }
   }
}
