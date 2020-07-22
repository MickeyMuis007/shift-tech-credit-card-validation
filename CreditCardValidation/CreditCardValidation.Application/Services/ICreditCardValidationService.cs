using System.Threading.Tasks;
using CreditCardValidation.Common.Models.CreditCards;

namespace CreditCardValidation.Application.Services {
  public interface ICreditCardValidationService {
    Task<CreditCardValidationResponse> ValidateCreditCardNo(CreditCardInsertDTO insertDTO);
  }
}