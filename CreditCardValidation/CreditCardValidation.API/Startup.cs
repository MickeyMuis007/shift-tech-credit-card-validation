using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IdentityServer4.AccessTokenValidation;

using CreditCardValidation.Domain.ApplicationUserAggreggate;
using CreditCardValidation.Persistence.Contexts;
using CreditCardValidation.API.Configurations.DIConfig;
namespace CreditCardValidation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddControllers(setupSetting => {
                    setupSetting.ReturnHttpNotAcceptable = true;
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction => {
                    setupAction.InvalidModelStateResponseFactory = context => {
                        // create a problem details object
                        var problemDetailsFactory = context.HttpContext.RequestServices
                            .GetRequiredService<ProblemDetailsFactory>();
                        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                            context.HttpContext,
                            context.ModelState
                        );

                        problemDetails.Detail = "See the errors field for details";
                        problemDetails.Instance = context.HttpContext.Request.Path;

                        // find out which status code to see
                        var actionExecutingContext = context as ActionExecutingContext;

                        // if there are modelstate errors & all arguments where correctly
                        // found/parsed we'er dealing with validation errors
                        if ((context.ModelState.ErrorCount > 0) &&
                            (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count)) {
                            problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            problemDetails.Title = "One or more validation errors occured";

                            return new UnprocessableEntityObjectResult(problemDetails) {
                                ContentTypes = { "application/problem+json" }
                            };
                        }

                        // if one of the argument wasn't correctly found / couldn't be parsed
                        // we're dealing with null/unparseable Input
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "One or more errors on input occured.";
                        return new BadRequestObjectResult(problemDetails) {
                            ContentTypes = { "applicatoin/problem+json" }
                        };

                    };
                });

            services.AddCors(o => o.AddPolicy("AllowedCorsPolicy", 
                builder => builder
                    .WithOrigins("http://localhost:4203")
                    .AllowAnyHeader()
                    .AllowAnyMethod()));

            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options => {
                    options.Authority = "http://localhost:5000";
                    options.ApiName = "cms-api";
                    options.ApiSecret = "secret";
                    options.RequireHttpsMetadata = false;
                });

            Console.WriteLine(Configuration["ConnectionString:CreditCardValidation"]);
            services.AddDbContext<CreditCardValidationDBContexts>(options => {
                options.UseMySql(Configuration["ConnectionString:CreditCardValidation"]);
            });

            services.AddIdentityCore<ApplicationUser>();

            services.AddScoped<IUserStore<ApplicationUser>, 
                UserOnlyStore<ApplicationUser, CreditCardValidationDBContexts>>();
            
            services.AddSwaggerGen(setupAction => {
                setupAction.SwaggerDoc(
                    "CreditCardOpenAPISpecification",
                    new OpenApiInfo()
                    {
                        Title = "Credit Card Validation API",
                        Version = "1",
                        Description = "This is Shift Tech Credit Card Validation API.",
                        Contact =  new OpenApiContact()
                        {
                            Email = "mihendricks1@gmail.com",
                            Name = "Michael Alan Hendricks",
                            Url = new Uri("https://github.com/MickeyMuis007")
                        }
                    }
                );
            });

            DIConfig.Configure(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint(
                    "/swagger/CreditCardOpenAPISpecification/swagger.json",
                    "Credit Card Validation API"
                );
                setupAction.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
