using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.API.Configurations.DIConfig.Tests;
using SharedKernel.Interfaces;
using Application.Services;


namespace CreditCardValidation.API.Configurations.DIConfig
{
  public class DIConfig
  {
		public static void Configure (IServiceCollection services)
    {
      services.AddTransient<IPropertyMappingService, PropertyMappingService>();
      services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();

      MappingProfileDIConfig.Configure(services);
      TestDIConfig.Configure(services);
    }

  }
}