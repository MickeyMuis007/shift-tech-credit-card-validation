using System;
using SharedKernel.Interfaces;
using CreditCardValidation.Common.Models.CreditCardStatuses;
using CreditCardValidation.Common.Models.CreditCardProviders;

namespace CreditCardValidation.Common.Models.CreditCards
{
	public class CreditCardDTO : IDTO
	{
		public Guid Id { get; set; }
		public string No { get; set; }
		public Guid CreditCardStatusId { get; set; }
		public Guid CreditCardProviderId { get; set; }
		public CreditCardStatusDTO CreditCardStatus { get; set; }
		public CreditCardProviderDTO CreditCardProvider { get; set; }
	}
}
