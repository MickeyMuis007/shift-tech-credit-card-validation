using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardValidation.Domain.CreditCardProviderAggregate;

namespace CreditCardValidation.Persistence.Configurations
{
	public class CreditCardProviderConfiguration : IEntityTypeConfiguration<CreditCardProvider>
	{
		public void Configure(EntityTypeBuilder<CreditCardProvider> builder)		{
			builder.ToTable("CreditCardProvider");
		}
	}
}