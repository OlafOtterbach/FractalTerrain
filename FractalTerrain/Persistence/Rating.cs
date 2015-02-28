/// <summary>Definition of the struct Rating.</summary>
/// <author>Olaf Otterbach</author>
/// <state>2015.02.26</state>

using System.Linq;

namespace FractalTerrain.Persistence
{
   public class Rating
   {
      public Rating()
      {
         this.GetType().GetProperties().Where(p => p.PropertyType == typeof(bool) && p.Name != "AllSatisfied").ToList().ForEach(p => p.SetValue(this, false));
      }

      public bool AllSatisfied
      {
         get
         {
            return this.GetType().GetProperties().Where(p => p.PropertyType == typeof(bool) && p.Name != "AllSatisfied").All(p => ( (bool)p.GetValue(this, null) ) == false);
         }
      }

      public bool HasParseError { get; set; }
      public bool HasMappingError { get; set; }
      public bool HasNoModel { get; set; }

      public bool NoModelToValidate { get; set; }
      public bool InvalidMapSize { get; set; }
      public bool InvalidAppleManSize { get; set; }


      public bool HasUnkownVersion { get; set; }
      public bool ResultIsEmpty { get; set; }
      public bool HasCorruptedData { get; set; }

      public bool CanNotSaveFile { get; set; }
      public bool CanNotOpenFile { get; set; }
   }
}
