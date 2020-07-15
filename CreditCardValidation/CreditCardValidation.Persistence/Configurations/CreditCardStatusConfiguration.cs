using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardValidation.Domain.CreditCardStatusAggregate;

namespace CreditCardValidation.Persistence.Configurations
{
	public class CreditCardStatusConfiguration : IEntityTypeConfiguration<CreditCardStatus>
	{
		public void Configure(EntityTypeBuilder<CreditCardStatus> builder)		{
			builder.ToTable("CreditCardStatus");
		}
	}
}