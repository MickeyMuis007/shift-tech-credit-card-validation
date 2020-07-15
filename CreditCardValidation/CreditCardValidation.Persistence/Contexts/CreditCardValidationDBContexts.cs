using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using CreditCardValidation.Persistence.Configuration;
using CreditCardValidation.Domain.ApplicationUserAggreggate;

namespace CreditCardValidation.Persistence.Contexts {
  public class CreditCardValidationDBContexts : IdentityDbContext<ApplicationUser> {
    public CMSDbContexts (DbContextOptions<CMSDbContexts> options): base(options) {

    }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfiguration(new TestConfiguration());
      base.OnModelCreating(modelBuilder);
    }
  }
}