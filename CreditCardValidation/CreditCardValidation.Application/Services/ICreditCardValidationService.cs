using CreditCardValidation.Common.Models.CreditCards;

namespace CreditCardValidation.Application.Services {
  public interface ICreditCardValidationService {
    bool ValidationCardNo(CreditCardInsertDTO insertDTO);
  }
}