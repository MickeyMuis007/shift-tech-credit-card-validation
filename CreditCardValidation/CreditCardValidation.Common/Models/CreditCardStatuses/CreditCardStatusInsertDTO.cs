using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Common.Models.CreditCardStatuses
{
	public class CreditCardStatusInsertDTO : ICreateDTO
	{
		public string Status { get; set; }
		public string Description { get; set; }
	}
}
