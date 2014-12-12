/// <summary>Definition of the class FileReader.</summary>
/// <author>Olaf Otterbach</author>

using FractalTerrain.Model;
using System;
using System.IO;

namespace FractalTerrain.Persistence
{
   public class FileReader : Reader
   {
      public new ReaderResult Read( string pathAndFileName )
      {
         var result = new ReaderResult();
         try
         {
            var text = System.IO.File.ReadAllText( pathAndFileName );
            result = base.Read( text );
         }
         catch( Exception e)
         {
            result.Rating.CanNotOpenFile = true;
         }
         return result;
      }
   }
}
