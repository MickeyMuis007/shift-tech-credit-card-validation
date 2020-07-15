using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.Domain.CreditCardStatusAggregate;
using CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCardStatuses;

namespace CreditCardValidation.API.Configurations.DIConfig.CreditCardStatuses
{
	public class CreditCardStatusDIConfig	{
		public static void Configure (IServiceCollection services)
		{
			services.AddScoped<CreditCardStatusBuilder, CreditCardStatusBuilder>();
			services.AddScoped<ICreditCardStatusRepository, CreditCardStatusRepository>();
			services.AddScoped<ICreditCardStatusUnitOfWork, CreditCardStatusUnitOfWork>();
		}
	}
}