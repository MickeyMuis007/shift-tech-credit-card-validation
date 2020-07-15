using System;
using System.Collections.Generic;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Domain.CreditCardStatusAggregate
{
	public class CreditCardStatus : CreditCardStatusBehaviour, IAggregateRoot
	{
		public string Status { get; private set; }
		public string Description { get; private set; }
		private CreditCardStatus () { }
		public CreditCardStatus (CreditCardStatusBuilder builder)
		{
			BehaviourInit(this, builder);
			Id = builder.Id;
			Status = builder.Status;
			Description = builder.Description;
		}
	}
}