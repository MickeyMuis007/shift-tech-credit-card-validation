using AutoMapper;
using CreditCardValidation.Common.Models.CreditCards;
using CreditCardValidation.Domain.CreditCardAggregate;

namespace CreditCardValidation.Application.Mappings
{
	public class CreditCardMappingProfile : Profile	{
		public CreditCardMappingProfile ()
		{
			CreateMap<CreditCard, CreditCardDTO>().ReverseMap();
			CreateMap<CreditCard, CreditCardInsertDTO>().ReverseMap();
			CreateMap<CreditCard, CreditCardUpdateDTO>().ReverseMap();
		}
	}
}
