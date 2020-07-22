using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardValidation.Application.Services;
using CreditCardValidation.Common.Enums;
using CreditCardValidation.Common.Models.CreditCards;
using CreditCardValidation.Domain.CreditCardAggregate;
using CreditCardValidation.Domain.CreditCardProviderAggregate;
using CreditCardValidation.Domain.CreditCardStatusAggregate;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Services.CreditCardValidationServices
{
  public class CreditCardValidationService : ICreditCardValidationService
  {
    private Action<CreditCardInsertDTO> _validateCreditCard;
    private readonly Dictionary<ValidateNoEnum, bool> _validatedResults;
    private readonly CreditCardBuilder _creditCardBuilder;
    private readonly CreditCardStatusBuilder _creditCardStatusBuilder;

    public CreditCardValidationService(CreditCardBuilder creditCardBuilder, CreditCardStatusBuilder creditCardStatusBuilder)
    {
      _validatedResults = new Dictionary<ValidateNoEnum, bool>();
      _creditCardStatusBuilder = creditCardStatusBuilder;
      _creditCardBuilder = creditCardBuilder;
    }
    public async Task<CreditCardValidationResponse> ValidateCreditCardNo(CreditCardInsertDTO model)
    {
      _validateCreditCard += LuhnValidation;
      _validateCreditCard += StartWithValidation;
      _validateCreditCard += LengthValidation;

      _validateCreditCard(model);
      await InserValidCreditCard(model);

      return new CreditCardValidationResponse(_validatedResults);
    }

    private void LuhnValidation(CreditCardInsertDTO model)
    {
      _validatedResults.Add(ValidateNoEnum.Luhn, true);
    }

    private void StartWithValidation(CreditCardInsertDTO model)
    {
      _validatedResults.Add(ValidateNoEnum.StartWith, true);
    }

    private void LengthValidation(CreditCardInsertDTO model)
    {
      _validatedResults.Add(ValidateNoEnum.Length, true);
    }

    private async Task InserValidCreditCard(CreditCardInsertDTO model)
    {
      if (!_validatedResults.ContainsValue(false))
      {
        var creditCardStatusesIssued = await _creditCardStatusBuilder.Build().GetAll(new CreditCardStatusQueryParams
        {
          SearchQuery = "Issued"
        });
        if (creditCardStatusesIssued != null && creditCardStatusesIssued.Count() > 0)
        {
          var creditCardStatusId = creditCardStatusesIssued.FirstOrDefault().Id;
          var creditCard = _creditCardBuilder.Copy(model).SetCreditCardStatusId(creditCardStatusId).Build();
          // await creditCard.Insert();
          Console.WriteLine("Insert Credit Card");
          Console.WriteLine($"No: {creditCard.No}");
          Console.WriteLine($"Credit Card Provider Id: {creditCard.CreditCardProviderId}");
          Console.WriteLine($"Credit Card Status Id: {creditCard.CreditCardStatusId}");
          Console.WriteLine($"Issued ID: {creditCardStatusId}");
        }
      }
    }
  }
}