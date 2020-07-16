using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardValidation.Domain.CreditCardProviderAggregate;

namespace CreditCardValidation.Persistence.Configurations
{
	public class CreditCardProviderConfiguration : IEntityTypeConfiguration<CreditCardProvider>
	{
		public void Configure(EntityTypeBuilder<CreditCardProvider> builder) {
			builder.ToTable("CreditCardProvider");

			var creditCardProviderBuilder = new CreditCardProviderBuilder(null, null);

			builder.HasData(
				creditCardProviderBuilder.SetId(new Guid("969cfded-d569-402b-8237-d3a6ac5c3eb3")).SetName("American Express").SetCode("AMEX").SetStartsWith("34,37").SetLength("15").Build(),
				creditCardProviderBuilder.SetId(new Guid("a7370c45-9429-4757-a45d-8fa1a2964474")).SetName("VISA").SetCode("VISA").SetStartsWith("4").SetLength("13,16,19").Build(),
				creditCardProviderBuilder.SetId(new Guid("2216cb5d-5acf-4d84-9741-43031d705acd")).SetName("MasterCard").SetCode("MasterCard").SetStartsWith("51,52,53,54,55,222100-272099").SetLength("16").Build(),
				creditCardProviderBuilder.SetId(new Guid("367d2e2c-95df-476d-92a8-2e7edc7e8e45")).SetName("Discover").SetCode("Discover").SetStartsWith("6011,622126-622925,644,645,646,647,648,649,65").SetLength("16,19").Build()
			);
		}
	}
}

/*
  Credit Card Providers:
		969cfded-d569-402b-8237-d3a6ac5c3eb3 - American Express
		a7370c45-9429-4757-a45d-8fa1a2964474 - VISA
		2216cb5d-5acf-4d84-9741-43031d705acd - Master Card
		367d2e2c-95df-476d-92a8-2e7edc7e8e45 - Discover
*/