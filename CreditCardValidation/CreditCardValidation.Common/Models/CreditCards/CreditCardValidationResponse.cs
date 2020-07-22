using System.Collections.Generic;
using CreditCardValidation.Common.Enums;
using System.Linq;

namespace CreditCardValidation.Common.Models.CreditCards
{
  public class CreditCardValidationResponse
  {
    public IEnumerable<object> ValidatedResponse { get; private set; }
    public bool Valid { get; private set; }

    public CreditCardValidationResponse(Dictionary<ValidateNoEnum, bool> validatedResponse)
    {
      ValidatedResponse = validatedResponse.Select( t => new {
        key = t.Key.ToString(),
        value = t.Value
      });

      Valid = !validatedResponse.ContainsValue(false);
    }


  }
}