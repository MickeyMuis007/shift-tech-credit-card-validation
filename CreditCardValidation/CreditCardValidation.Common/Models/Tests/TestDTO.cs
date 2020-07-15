using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Common.Models.TestModels
{
	public class TestDTO : IDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime? BirthDate { get; set; }
	}
}
