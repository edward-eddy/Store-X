using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store_X.Domain.Contracts;
using Store_X.Domain.Entities.Identity;
using Store_X.Persistence;
using Store_X.Persistence.Identity.Contexts;
using Store_X.Services;
using Store_X.Shared;
using Store_X.Shared.ErrorModels;
using Store_X.Web.Middlewares;
using System.Text;

namespace Store_X.Web.Extentions
{
    public static class Extentions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebServices();

            services.AddInfraStructureServices(configuration);

            services.AddApplicationServices(configuration);

            services.ConfigureApiBehaviorOptions();

            services.AddIdentityServices();

            services.Configure<JwtOptions>(configuration.GetSection("JwrOptions"));

            services.AddAuthenticationService(configuration);


            return services;
        }

        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Any())
                                                         .Select(M => new ValidationError()
                                                         {
                                                             Field = M.Key,
                                                             Errors = M.Value.Errors.Select(errors => errors.ErrorMessage)
                                                         });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(optiom =>
            {
                optiom.DefaultAuthenticateScheme = "Bearer";
                optiom.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),

                };
            });

            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;

            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityStoreDbContext>();
            return services;
        }




        public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
        {
            await app.SeedData();

            app.UseGlobalErrorHandling();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Ask CLR To Create Object From IDbInitializer
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }
    }
}
