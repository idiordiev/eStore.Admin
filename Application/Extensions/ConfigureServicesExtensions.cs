using System.Reflection;
using eStore_Admin.Application.Filtering.Factories;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Mapping;
using eStore_Admin.Application.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace eStore_Admin.Application.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddAutomapperWithProfiles();
            services.AddFilterExpressionFactories();
            services.AddValidation();
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        private static void AddAutomapperWithProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddFilterExpressionFactories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerFilterExpressionFactory, CustomerFilterExpressionFactory>();
            services.AddScoped<IGamepadFilterExpressionFactory, GamepadFilterExpressionFactory>();
            services.AddScoped<IKeyboardFilterExpressionFactory, KeyboardFilterExpressionFactory>();
        }

        private static void AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}