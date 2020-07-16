using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.Domain.CreditCardProviderAggregate;
using CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCardProviders;

namespace CreditCardValidation.API.Configurations.DIConfig.CreditCardProviders
{
	public class CreditCardProviderDIConfig	{
		public static void Configure (IServiceCollection services)
		{
			services.AddScoped<CreditCardProviderBuilder, CreditCardProviderBuilder>();
			services.AddScoped<ICreditCardProviderRepository, CreditCardProviderRepository>();
			services.AddScoped<ICreditCardProviderUnitOfWork, CreditCardProviderUnitOfWork>();
		}
	}
}