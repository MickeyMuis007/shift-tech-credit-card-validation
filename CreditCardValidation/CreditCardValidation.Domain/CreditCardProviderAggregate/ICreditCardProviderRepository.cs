using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Domain.CreditCardProviderAggregate
{
	public interface ICreditCardProviderRepository : IRepository<CreditCardProvider, Guid, CreditCardProviderQueryParams>
	{
		bool Exists(Guid id);
	}
}