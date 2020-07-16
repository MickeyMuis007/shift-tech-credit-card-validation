using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.Domain.CreditCardAggregate;
using CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCards;

namespace CreditCardValidation.API.Configurations.DIConfig.CreditCards
{
	public class CreditCardDIConfig	{
		public static void Configure (IServiceCollection services)
		{
			services.AddScoped<CreditCardBuilder, CreditCardBuilder>();
			services.AddScoped<ICreditCardRepository, CreditCardRepository>();
			services.AddScoped<ICreditCardUnitOfWork, CreditCardUnitOfWork>();
		}
	}
}