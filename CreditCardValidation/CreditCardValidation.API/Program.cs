using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CreditCardValidation.Persistence.Contexts;

namespace CreditCardValidation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope()) {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                try {
                    var context = scope.ServiceProvider.GetService<CreditCardValidationDBContexts>();

                    // context.Database.EnsureDeleted();
                    context.Database.Migrate();
                    logger.LogInformation("Migration Completed!");
                } catch(Exception ex) {
                    logger.LogError(ex, "An error occured while migrating the database");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
