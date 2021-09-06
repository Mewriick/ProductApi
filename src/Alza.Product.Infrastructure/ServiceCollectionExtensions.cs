using Alza.Product.Domain;
using Alza.Product.Infrastructure.ReadModels;
using Alza.Product.ReadModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Alza.Product.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ProductRepository>();
            services.AddScoped<IProductRepository, ProductRepositoryWithSync>(
                provier => new ProductRepositoryWithSync(
                    provier.GetRequiredService<ProductRepository>(),
                    provier.GetRequiredService<IProductSynchronizer>()));

            services.AddDbContext<EventStore>(x => x.UseInMemoryDatabase("localEventStore"));
            services.AddDbContext<ProductsReadDbContext>(x => x.UseInMemoryDatabase("localReadModel"));

            services.AddEventSourcing(builder =>
            {
                builder.UseEfCoreEventStore<EventStore, Domain.Product, Guid>();
            });

            services.AddScoped<IProductsQueryable, ProductsQueryable>();
            services.AddScoped<IProductSynchronizer, ProductSynchronizer>();

            return services;
        }
    }
}
