using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.Domain.CreditCardAggregate;
using CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCards;
using CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Services.CreditCardValidationServices;
using CreditCardValidation.Application.Services;

namespace CreditCardValidation.API.Configurations.DIConfig.CreditCards
{
	public class CreditCardDIConfig	{
		public static void Configure (IServiceCollection services)
		{
			services.AddScoped<CreditCardBuilder, CreditCardBuilder>();
			services.AddScoped<ICreditCardRepository, CreditCardRepository>();
			services.AddScoped<ICreditCardUnitOfWork, CreditCardUnitOfWork>();
			services.AddScoped<ICreditCardValidationService, CreditCardValidationService>();
		}
	}
}