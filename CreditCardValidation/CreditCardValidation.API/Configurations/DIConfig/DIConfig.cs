using Microsoft.Extensions.DependencyInjection;
using CreditCardValidation.API.Configurations.DIConfig.Tests;
using CreditCardValidation.API.Configurations.DIConfig.CreditCardStatuses;
using CreditCardValidation.API.Configurations.DIConfig.CreditCardProviders;
using CreditCardValidation.API.Configurations.DIConfig.CreditCards;
using SharedKernel.Interfaces;
using Application.Services;
using CreditCardValidation.Application.Workers;


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
      CreditCardStatusDIConfig.Configure(services);
      CreditCardProviderDIConfig.Configure(services);
      CreditCardDIConfig.Configure(services);
      services.AddHostedService<ProcessCreditCard>();
    }

  }
}