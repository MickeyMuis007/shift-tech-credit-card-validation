using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.API.Configurations.DIConfig.Tests;

namespace CreditCardValidation.API.Configurations.DIConfig
{
  public class DIConfig
  {
		public static void Configure (IServiceCollection services)
    {
      TestDIConfig.Configure(services);
    }

  }
}