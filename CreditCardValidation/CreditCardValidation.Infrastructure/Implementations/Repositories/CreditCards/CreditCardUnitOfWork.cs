using System.Threading.Tasks;
using CreditCardValidation.Domain.CreditCardAggregate;
using CreditCardValidation.Persistence.Contexts;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCards
{
	public class CreditCardUnitOfWork : ICreditCardUnitOfWork	{
		private readonly CreditCardValidationDBContexts _db;
		private readonly ICreditCardRepository _creditCardRepository;

		public CreditCardUnitOfWork (CreditCardValidationDBContexts db, ICreditCardRepository creditCardRepository)		{
			_creditCardRepository = creditCardRepository;
			_db = db;
		}

		public ICreditCardRepository CreditCardRepository
		{
			get
			{
				return _creditCardRepository;
			}
		}
		public async Task<int> SaveAsync()
		{
			return await _db.SaveChangesAsync();
		}
	}
}
