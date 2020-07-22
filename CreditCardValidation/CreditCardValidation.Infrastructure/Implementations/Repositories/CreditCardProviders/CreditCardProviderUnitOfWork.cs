using System.Threading.Tasks;
using CreditCardValidation.Domain.CreditCardProviderAggregate;
using CreditCardValidation.Persistence.Contexts;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCardProviders
{
	public class CreditCardProviderUnitOfWork : ICreditCardProviderUnitOfWork	{
		private readonly CreditCardValidationDBContexts _db;
		private readonly ICreditCardProviderRepository _creditCardProviderRepository;

		public CreditCardProviderUnitOfWork (CreditCardValidationDBContexts db, ICreditCardProviderRepository creditCardProviderRepository)		{
			_creditCardProviderRepository = creditCardProviderRepository;
			_db = db;
		}

		public ICreditCardProviderRepository CreditCardProviderRepository
		{
			get
			{
				return _creditCardProviderRepository;
			}
		}
		public async Task<int> SaveAsync()
		{
			return await _db.SaveChangesAsync();
		}
	}
}
