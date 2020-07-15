using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreditCardValidation.Domain.TestAggregate;

namespace CreditCardValidation.Persistence.Configurations
{
	public class TestConfiguration : IEntityTypeConfiguration<Test>
	{
		public void Configure(EntityTypeBuilder<Test> builder)		{
			builder.ToTable("Test");
		}
	}
}