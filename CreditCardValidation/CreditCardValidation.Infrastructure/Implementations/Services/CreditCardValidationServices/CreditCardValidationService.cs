using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CreditCardValidation.Application.Services;
using CreditCardValidation.Common.Enums;
using CreditCardValidation.Common.Models.CreditCardProviders;
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
    private readonly CreditCardProviderBuilder _creditCardProviderBuilder;
    private CreditCardProviderDTO _creditCardProviderDTO;

    public CreditCardValidationService(CreditCardBuilder creditCardBuilder, CreditCardStatusBuilder creditCardStatusBuilder,
      CreditCardProviderBuilder creditCardProviderBuilder)
    {
      _validatedResults = new Dictionary<ValidateNoEnum, bool>();
      _creditCardStatusBuilder = creditCardStatusBuilder;
      _creditCardBuilder = creditCardBuilder;
      _creditCardProviderBuilder = creditCardProviderBuilder;
    }
    public async Task<CreditCardValidationResponse> ValidateCreditCardNo(CreditCardInsertDTO model)
    {
      _creditCardProviderDTO = await _creditCardProviderBuilder.Build().GetById(model.CreditCardProviderId);
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
      var startWithList = _creditCardProviderDTO.StartsWith.Split(",");
      foreach (var startWith in startWithList)
      {
        var item = startWith.Trim();
        if (item.Contains("-"))
        {
          var rangeList = item.Split("-");
          var minStr = rangeList[0].Trim();
          var maxStr = rangeList[1].Trim();
          int min;
          int max;
          if (!int.TryParse(minStr, out min))
          {
            throw new Exception("Min range is invalid");
          }
          if (!int.TryParse(maxStr, out max))
          {
            throw new Exception("Max range invalid");
          }
          Console.WriteLine($"Min: {min}");
          Console.WriteLine($"Max: {max}");
          var noMinStr = model.No.Substring(0, minStr.Count());
          var noMaxStr = model.No.Substring(0, maxStr.Count());
          int noMin, noMax;

          if (!int.TryParse(noMinStr, out noMin))
          {
            throw new Exception("No min range is invalid");
          }

          if (!int.TryParse(noMaxStr, out noMax))
          {
            throw new Exception("No max range is invalid");
          }

          Console.WriteLine($"No min: {noMin}");
          Console.WriteLine($"No max: {noMax}");
          if (noMax >= min && noMax <= max)
          {
            _validatedResults.Add(ValidateNoEnum.StartWith, true);
            Console.WriteLine($"InRange: true");
            return;
          }
        }

        Console.WriteLine($"Start With: {item} - {model.No.StartsWith(item)}");
        if (model.No.StartsWith(item))
        {
          _validatedResults.Add(ValidateNoEnum.StartWith, true);
          return;
        }
      }
      _validatedResults.Add(ValidateNoEnum.StartWith, false);
    }

    private void LengthValidation(CreditCardInsertDTO model)
    {
      var lengthDict = GetLengthList();
      Console.WriteLine($"Length: {model.No.Count()} - {lengthDict.ContainsKey(model.No.Count())}");
      if (!lengthDict.ContainsKey(model.No.Count()))
        _validatedResults.Add(ValidateNoEnum.Length, false);
      else
        _validatedResults.Add(ValidateNoEnum.Length, true);
    }

    private Dictionary<int, bool> GetLengthList()
    {
      var output = new Dictionary<int, bool>();
      var lengthList = _creditCardProviderDTO.Length.Split(",");
      int length;
      foreach (var item in lengthList)
      {
        var cleanItem = item.Trim();
        if (!int.TryParse(cleanItem, out length))
          throw new Exception($"Invalid Length set for provider {_creditCardProviderDTO.Code}");
        output.Add(length, true);
      }

      return output;
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
          await creditCard.Insert();
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