using System.IO;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Infrastructure.Logging;
using eStore_Admin.Infrastructure.Persistence;
using eStore_Admin.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace eStore_Admin.Infrastructure.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDbContext(configuration);
            services.AddUnitOfWork();
            services.AddLoggingService();
            services.AddDateTimeService();
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
    }
}