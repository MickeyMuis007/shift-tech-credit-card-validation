using System;
using SharedKernel.Models;

namespace CreditCardValidation.Domain.CreditCardAggregate
{
	public class CreditCardQueryParams : QueryParams
	{
		public Guid? CreditCardStatusId { get; set; }
	}
}