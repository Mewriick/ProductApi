using Alza.Product.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Alza.Product.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(currentAssembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
