using System.Collections.Generic;
using System;
using CreditCardValidation.Application.Services;
using CreditCardValidation.Common.Models.CreditCards;
using CreditCardValidation.Common.Enums;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Services.CreditCardValidationServices {
  public class CreditCardValidationService : ICreditCardValidationService {
    private Action<CreditCardInsertDTO> _validateCreditCard;
    private Dictionary<ValidateNoEnum, bool> _validatedResults;

    public CreditCardValidationService() {
      _validatedResults = new Dictionary<ValidateNoEnum, bool>();
    }
    public CreditCardValidationResponse ValidateCreditCardNo(CreditCardInsertDTO model) {
      _validateCreditCard += LuhnValidation;
      _validateCreditCard += StartWithValidation;
      _validateCreditCard += LengthValidation;

      _validateCreditCard(model);

      return new CreditCardValidationResponse(_validatedResults);
    }

    private void LuhnValidation(CreditCardInsertDTO model) {
      _validatedResults.Add(ValidateNoEnum.Luhn, true);
    }

    private void StartWithValidation(CreditCardInsertDTO model) {
      _validatedResults.Add(ValidateNoEnum.StartWith, true);
    }

    private void LengthValidation(CreditCardInsertDTO model) {
      _validatedResults.Add(ValidateNoEnum.Length, true);
    }
  }
}