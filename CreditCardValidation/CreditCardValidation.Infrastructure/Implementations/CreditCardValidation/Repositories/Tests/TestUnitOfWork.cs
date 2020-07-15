using System.Threading.Tasks;
using CreditCardValidation.Domain.TestAggregate;
using CreditCardValidation.Persistence.Contexts;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.Tests
{
	public class TestUnitOfWork : ITestUnitOfWork	{
		private readonly CreditCardValidationDBContexts _db;
		private readonly ITestRepository _testRepository;

		public TestUnitOfWork (CreditCardValidationDBContexts db, ITestRepository testRepository)		{
			_testRepository = testRepository;
			_db = db;
		}

		public ITestRepository TestRepository
		{
			get
			{
				return _testRepository;
			}
		}
		public async Task<int> SaveAsync()
		{
			return await _db.SaveChangesAsync();
		}
	}
}
