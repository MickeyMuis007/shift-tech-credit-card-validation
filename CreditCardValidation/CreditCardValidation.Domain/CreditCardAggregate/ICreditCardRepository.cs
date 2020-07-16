using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Domain.CreditCardAggregate
{
	public interface ICreditCardRepository : IRepository<CreditCard, Guid, CreditCardQueryParams>
	{
		bool Exists(Guid id);
	}
}