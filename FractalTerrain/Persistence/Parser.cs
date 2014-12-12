/// <summary>Definition of the class Parser.</summary>
/// <author>Olaf Otterbach</author>

using System.Linq;
using System.Text.RegularExpressions;

namespace FractalTerrain.Persistence
{
   public class Parser
   {
      public Context Parse(Context ctx )
      {
         var pattern = @"^(?:\[)(?<Section>[^\]]*)(?:\])(?:[\s\r\n]+)((?<Key>[^\s\[]+)(?:[^\S\[\r\n]+)(?<Value>[^\s\[]+)(?:[\r\n]+))*(?:[\r\n\s]*)";
         try
         {
            var matches = Regex.Matches(ctx.Text, pattern, RegexOptions.Multiline);
            var res = matches.Cast<Match>()
                             .Select
                             (
                                match => new
                                {
                                   Section = match.Groups["Section"].Value,
                                   KeyValues
                                   = match.Groups["Key"].Captures
                                                        .Cast<Capture>()
                                                        .Zip(match.Groups["Value"]
                                                        .Captures
                                                        .Cast<Capture>()
                                                        .Select(x => x.Value), (key, value) => new { Key = key.Value, Value = value })
                                                        .ToDictionary(pair => pair.Key, pair => pair.Value)
                                }
                             ).ToDictionary(section => section.Section, section => section.KeyValues);
            var data = new ParserData()
            {
               FileType = res["Header"]["FileType"],
               Version = res["Header"]["Version"],
               Data = res
            };
            ctx.ParserData = data;
         }
         catch
         {
            ctx.Rating.HasParseError = true;
         }
         return ctx;
      }
   }
}
