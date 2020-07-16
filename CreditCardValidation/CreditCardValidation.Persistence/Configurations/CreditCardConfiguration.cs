using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardValidation.Domain.CreditCardAggregate;

namespace CreditCardValidation.Persistence.Configurations
{
	public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
	{
		public void Configure(EntityTypeBuilder<CreditCard> builder)		{
			builder.ToTable("CreditCard");
		}
	}
}