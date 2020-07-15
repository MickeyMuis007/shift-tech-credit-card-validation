using System.Threading.Tasks;
using CreditCardValidation.Domain.CreditCardStatusAggregate;
using CreditCardValidation.Persistence.Contexts;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCardStatuses
{
	public class CreditCardStatusUnitOfWork : ICreditCardStatusUnitOfWork	{
		private readonly CreditCardValidationDBContexts _db;
		private readonly ICreditCardStatusRepository _creditCardStatusRepository;

		public CreditCardStatusUnitOfWork (CreditCardValidationDBContexts db, ICreditCardStatusRepository creditCardStatusRepository)		{
			_creditCardStatusRepository = creditCardStatusRepository;
			_db = db;
		}

		public ICreditCardStatusRepository CreditCardStatusRepository
		{
			get
			{
				return _creditCardStatusRepository;
			}
		}
		public async Task<int> SaveAsync()
		{
			return await _db.SaveChangesAsync();
		}
	}
}
