using System.Threading.Tasks;

namespace CreditCardValidation.Domain.CreditCardStatusAggregate
{
	public interface ICreditCardStatusUnitOfWork
	{
		ICreditCardStatusRepository CreditCardStatusRepository { get; }
		Task<int> SaveAsync();
	}
}