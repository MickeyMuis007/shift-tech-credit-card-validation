using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Common.Models.CreditCardStatuses
{
	public class CreditCardStatusDTO : IDTO
	{
		public Guid Id { get; set; }
		public string Status { get; set; }
		public string Description { get; set; }
	}
}
