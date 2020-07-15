using System.ComponentModel;

namespace CreditCardValidation.API.Enums {
  public enum ResponseMessageEnum {
    [Description("Request successul.")]
    Success,
    [Description("Request responded with exceptions.")]
    Exception,
    [Description("Request denied")]
    UnAuthorized,
    [Description("Request responded with validation error(s).")]
    ValidationError,
    [Description("Unable to process the request")]
    Failure
  }
}