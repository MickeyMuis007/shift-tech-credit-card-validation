using AutoMapper;
using CreditCardValidation.Common.Models.TestModels;
using CreditCardValidation.Domain.TestAggregate;

namespace CreditCardValidation.Application.Mappings
{
	public class TestMappingProfile : Profile	{
		public TestMappingProfile ()
		{
			CreateMap<Test, TestDTO>().ReverseMap();
			CreateMap<Test, TestInsertDTO>().ReverseMap();
			CreateMap<Test, TestUpdateDTO>().ReverseMap();
		}
	}
}
