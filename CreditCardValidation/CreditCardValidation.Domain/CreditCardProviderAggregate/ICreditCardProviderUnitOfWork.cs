using System.Threading.Tasks;

namespace CreditCardValidation.Domain.CreditCardProviderAggregate
{
	public interface ICreditCardProviderUnitOfWork
	{
		ICreditCardProviderRepository CreditCardProviderRepository { get; }
		Task<int> SaveAsync();
	}
}