using CreditCardValidation.Common.Models.CreditCards;

namespace CreditCardValidation.Application.Services {
  public interface ICreditCardValidationService {
    CreditCardValidationResponse ValidateCreditCardNo(CreditCardInsertDTO insertDTO);
  }
}