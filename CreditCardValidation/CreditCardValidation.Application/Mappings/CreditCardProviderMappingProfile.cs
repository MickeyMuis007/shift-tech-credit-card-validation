using AutoMapper;
using CreditCardValidation.Common.Models.CreditCardProviders;
using CreditCardValidation.Domain.CreditCardProviderAggregate;

namespace CreditCardValidation.Application.Mappings
{
	public class CreditCardProviderMappingProfile : Profile	{
		public CreditCardProviderMappingProfile ()
		{
			CreateMap<CreditCardProvider, CreditCardProviderDTO>().ReverseMap();
			CreateMap<CreditCardProvider, CreditCardProviderInsertDTO>().ReverseMap();
			CreateMap<CreditCardProvider, CreditCardProviderUpdateDTO>().ReverseMap();
		}
	}
}
