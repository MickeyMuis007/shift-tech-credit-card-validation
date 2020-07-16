using System;
using System.Collections.Generic;
using SharedKernel.Interfaces;
using CreditCardValidation.Domain.CreditCardStatusAggregate;
using CreditCardValidation.Domain.CreditCardProviderAggregate;

namespace CreditCardValidation.Domain.CreditCardAggregate
{
	public class CreditCard : CreditCardBehaviour, IAggregateRoot
	{
		public string No { get; private set; }
		public Guid CreditCardStatusId { get; private set; }
		public Guid CreditCardProviderId { get; private set; }
		public CreditCardStatus CreditCardStatus { get; private set; }
		public CreditCardProvider CreditCardProvider { get; private set; }
		private CreditCard () { }
		public CreditCard (CreditCardBuilder builder)
		{
			BehaviourInit(this, builder);
			Id = builder.Id;
			No = builder.No;
			CreditCardStatusId = builder.CreditCardStatusId;
			CreditCardProviderId = builder.CreditCardProviderId;
			CreditCardStatus = builder.CreditCardStatus;
			CreditCardProvider = builder.CreditCardProvider;
		}
	}
}