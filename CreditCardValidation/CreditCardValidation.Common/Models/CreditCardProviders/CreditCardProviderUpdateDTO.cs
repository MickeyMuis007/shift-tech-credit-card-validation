using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Common.Models.CreditCardProviders
{
	public class CreditCardProviderUpdateDTO : IUpdateDTO
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public string StartsWith { get; set; }
		public string Length { get; set; }
	}
}
