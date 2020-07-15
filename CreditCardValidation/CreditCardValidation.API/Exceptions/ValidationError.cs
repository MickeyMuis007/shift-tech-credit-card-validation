using Newtonsoft.Json;

namespace CreditCardValidation.API.Exceptions {
  public class ValidationError {
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Field { get; }
    public string Message { get; }
    public ValidationError(string field, string message) {
      Field = field != string.Empty ? field : null;
      Message = message;
    }
  }
}