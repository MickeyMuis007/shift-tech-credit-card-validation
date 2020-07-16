using System.Threading.Tasks;

namespace CreditCardValidation.Domain.CreditCardAggregate
{
	public interface ICreditCardUnitOfWork
	{
		ICreditCardRepository CreditCardRepository { get; }
		Task<int> SaveAsync();
	}
}