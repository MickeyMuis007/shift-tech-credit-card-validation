using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardValidation.Domain.CreditCardAggregate;
using System;

namespace CreditCardValidation.Persistence.Configurations
{
	public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
	{
		public void Configure(EntityTypeBuilder<CreditCard> builder)		{
			builder.ToTable("CreditCard");

			var creditCardBuilder = new CreditCardBuilder(null, null);

			builder.HasData(
				creditCardBuilder.SetId(new Guid("d843d16c-79fa-477f-a08a-ed19ab3d96f1")).SetNo("374245455400126").SetCreditCardProviderId(new Guid("969cfded-d569-402b-8237-d3a6ac5c3eb3")).SetCreditCardStatusId(new Guid("30eaec92-2531-4634-b14e-3492a578edb9")).Build(),
				creditCardBuilder.SetId(new Guid("0fcfd9be-fceb-4b48-810c-6b74003ab757")).SetNo("60115564485789458").SetCreditCardProviderId(new Guid("367d2e2c-95df-476d-92a8-2e7edc7e8e45")).SetCreditCardStatusId(new Guid("30eaec92-2531-4634-b14e-3492a578edb9")).Build(),
				creditCardBuilder.SetId(new Guid("c18a7a12-aa81-4fe4-b0e9-55cc9b695a7c")).SetNo("5425233430109903").SetCreditCardProviderId(new Guid("2216cb5d-5acf-4d84-9741-43031d705acd")).SetCreditCardStatusId(new Guid("30eaec92-2531-4634-b14e-3492a578edb9")).Build(),
				creditCardBuilder.SetId(new Guid("aa360156-4e11-44ea-b0ad-cc8376874c75")).SetNo("4263982640269299").SetCreditCardProviderId(new Guid("a7370c45-9429-4757-a45d-8fa1a2964474")).SetCreditCardStatusId(new Guid("30eaec92-2531-4634-b14e-3492a578edb9")).Build()
			);
		}
	}
}

/*
	Credit Card:
		d843d16c-79fa-477f-a08a-ed19ab3d96f1
		0fcfd9be-fceb-4b48-810c-6b74003ab757
		c18a7a12-aa81-4fe4-b0e9-55cc9b695a7c
		aa360156-4e11-44ea-b0ad-cc8376874c75

  Credit Card Providers:
		969cfded-d569-402b-8237-d3a6ac5c3eb3 - American Express
		a7370c45-9429-4757-a45d-8fa1a2964474 - VISA
		2216cb5d-5acf-4d84-9741-43031d705acd - Master Card
		367d2e2c-95df-476d-92a8-2e7edc7e8e45 - Discover
	
	Credit Card Status:
		30eaec92-2531-4634-b14e-3492a578edb9 - Issued
		ec85b303-efa1-4bac-b2f5-8aa927f87df2 - Processed
*/