using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardValidation.Domain.CreditCardStatusAggregate;
using System;

namespace CreditCardValidation.Persistence.Configurations
{
	public class CreditCardStatusConfiguration : IEntityTypeConfiguration<CreditCardStatus>
	{
		public void Configure(EntityTypeBuilder<CreditCardStatus> builder)		{
			builder.ToTable("CreditCardStatus");

			var creditCardStatusBuilder = new CreditCardStatusBuilder(null, null);

			builder.HasData(
				creditCardStatusBuilder.SetId(new Guid("30eaec92-2531-4634-b14e-3492a578edb9")).SetStatus("Issued").SetDescription("Valid approved credit cards, that's required to be processed.").Build(),
				creditCardStatusBuilder.SetId(new Guid("ec85b303-efa1-4bac-b2f5-8aa927f87df2")).SetStatus("Processed").SetDescription("Credit cards that has been processed.").Build()
			);
		}
	}
}

/*
	Credit Card Status:
		30eaec92-2531-4634-b14e-3492a578edb9 - Issued
		ec85b303-efa1-4bac-b2f5-8aa927f87df2 - Processed
*/