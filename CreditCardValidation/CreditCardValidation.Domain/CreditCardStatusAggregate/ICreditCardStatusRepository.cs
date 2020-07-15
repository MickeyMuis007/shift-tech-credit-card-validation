using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Domain.CreditCardStatusAggregate
{
	public interface ICreditCardStatusRepository : IRepository<CreditCardStatus, Guid, CreditCardStatusQueryParams>
	{
	}
}