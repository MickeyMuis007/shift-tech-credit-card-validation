using System.Threading.Tasks;

namespace CreditCardValidation.Domain.TestAggregate
{
	public interface ITestUnitOfWork
	{
		ITestRepository TestRepository { get; }
		Task<int> SaveAsync();
	}
}