using Microsoft.Extensions.DependencyInjection;

using AutoMapper;

using CreditCardValidation.Application.Mappings;

namespace CreditCardValidation.API.Configurations.DIConfig {
  public class MappingProfileDIConfig {
    public static void Configure(IServiceCollection services) {
      services.AddAutoMapper(cfg => {
        cfg.AddProfile<TestMappingProfile>();
        cfg.AddProfile<CreditCardStatusMappingProfile>();
        cfg.AddProfile<CreditCardProviderMappingProfile>();
        cfg.AddProfile<CreditCardMappingProfile>();
      },typeof(Startup));
    }
  }
}