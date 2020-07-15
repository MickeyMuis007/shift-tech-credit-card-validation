using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.Domain.TestAggregate;
using CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.Tests;

namespace CreditCardValidation.API.Configurations.DIConfig.Tests
{
	public class TestDIConfig	{
		public static void Configure (IServiceCollection services)
		{
			services.AddScoped<TestBuilder, TestBuilder>();
			services.AddScoped<ITestRepository, TestRepository>();
			services.AddScoped<ITestUnitOfWork, TestUnitOfWork>();
		}
	}
}