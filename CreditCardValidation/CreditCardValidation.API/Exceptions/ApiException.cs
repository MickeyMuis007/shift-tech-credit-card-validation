using System;
using System.Collections.Generic;

namespace CreditCardValidation.API.Exceptions {
  public class ApiException : Exception {
    public int StatusCode { get; set; }
    public IEnumerable<ValidationError> Errors { get; set; }
    public string ReferenceErrorCode { get; set; }
    public string ReferenceDocumentLink { get; set; }
    public ApiException (string message, int statusCode = 500, IEnumerable<ValidationError> errors = null,
      string errorCode = "", string refLink = "") : base (message) {

      StatusCode = statusCode;
      Errors = errors;
      ReferenceErrorCode = errorCode;
      ReferenceDocumentLink = refLink;
    }

    public ApiException(Exception exception, int statusCode = 500) : base(exception.Message) {
      StatusCode = statusCode;
    }
  }
}