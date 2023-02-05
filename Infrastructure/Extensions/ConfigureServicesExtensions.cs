using System;
using System.IO;
using System.Text;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Infrastructure.Identity;
using eStore_Admin.Infrastructure.Logging;
using eStore_Admin.Infrastructure.Persistence;
using eStore_Admin.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog;

namespace eStore_Admin.Infrastructure.Extensions;

public static class ConfigureServicesExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDbContext(configuration);
        services.AddUnitOfWork();
        services.AddLoggingService();
        services.AddDateTimeService();
        services.AddAuthService();
        services.AddIdentity(configuration);
        services.AddJwt(configuration);
    }

    private static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ApplicationContext"));
        });
    }

    private static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddLoggingService(this IServiceCollection services)
    {
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        services.AddSingleton<ILogger>(_ => LogManager.GetCurrentClassLogger());
        services.AddSingleton<ILoggingService, LoggingService>();
    }

    private static void AddDateTimeService(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeService, DateTimeService>();
    }

    private static void AddAuthService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }

    private static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityContext"));
        });
        IdentityBuilder builder = services.AddIdentityCore<IdentityUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 10;
            options.User.RequireUniqueEmail = true;
        });

        builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
        builder.AddEntityFrameworkStores<IdentityContext>();
        builder.AddDefaultTokenProviders();
    }

    private static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtSetting));

        IConfigurationSection jwtSettings = configuration.GetSection(JwtSettings.JwtSetting);
        string secretKey = Environment.GetEnvironmentVariable("SECRET");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }
}