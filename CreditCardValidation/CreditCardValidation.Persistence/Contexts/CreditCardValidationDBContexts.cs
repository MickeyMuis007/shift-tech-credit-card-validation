using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using CreditCardValidation.Persistence.Configurations;
using CreditCardValidation.Domain.ApplicationUserAggreggate;
using CreditCardValidation.Domain.TestAggregate;
using CreditCardValidation.Domain.CreditCardStatusAggregate;

namespace CreditCardValidation.Persistence.Contexts {
  public class CreditCardValidationDBContexts : IdentityDbContext<ApplicationUser> {
    public CreditCardValidationDBContexts (DbContextOptions<CreditCardValidationDBContexts> options): base(options) {

    }

    public virtual DbSet<Test> Tests { get; set; }
    public virtual DbSet<CreditCardStatus> CreditCardStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfiguration(new TestConfiguration());
      base.OnModelCreating(modelBuilder);
    }
  }
}