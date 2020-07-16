using System;
using System.Collections.Generic;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Domain.CreditCardProviderAggregate
{
	public class CreditCardProvider : CreditCardProviderBehaviour, IAggregateRoot
	{
		public string Name { get; private set; }
		public string Code { get; private set; }
		public string StartsWith { get; private set; }
		public string Length { get; private set; }
		private CreditCardProvider () { }
		public CreditCardProvider (CreditCardProviderBuilder builder)
		{
			BehaviourInit(this, builder);
			Id = builder.Id;
			Name = builder.Name;
			Code = builder.Code;
			StartsWith = builder.StartsWith;
			Length = builder.Length;
		}
	}
}