using System;
using System.Collections.Generic;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Domain.TestAggregate
{
	public class Test : TestBehaviour, IAggregateRoot
	{
		public string Name { get; private set; }
		public string LastName { get; private set; }
		public string PhoneNumber { get; private set; }
		public DateTime? BirthDate { get; private set; }
		private Test () { }
		public Test (TestBuilder builder)
		{
			BehaviourInit(this, builder);
			Id = builder.Id;
			Name = builder.Name;
			LastName = builder.LastName;
			PhoneNumber = builder.PhoneNumber;
			BirthDate = builder.BirthDate;
		}
	}
}