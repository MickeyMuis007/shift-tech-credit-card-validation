using AutoMapper;
using CreditCardValidation.Common.Models.CreditCardStatuses;
using CreditCardValidation.Domain.CreditCardStatusAggregate;

namespace CreditCardValidation.Application.Mappings
{
	public class CreditCardStatusMappingProfile : Profile	{
		public CreditCardStatusMappingProfile ()
		{
			CreateMap<CreditCardStatus, CreditCardStatusDTO>().ReverseMap();
			CreateMap<CreditCardStatus, CreditCardStatusInsertDTO>().ReverseMap();
			CreateMap<CreditCardStatus, CreditCardStatusUpdateDTO>().ReverseMap();
		}
	}
}
