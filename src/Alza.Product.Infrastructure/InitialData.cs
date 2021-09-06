using Alza.Product.Domain.Events;
using Alza.Product.Infrastructure.ReadModels;
using JKang.EventSourcing.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alza.Product.Infrastructure
{
    public static class InitialData
    {
        private static readonly JsonSerializerSettings Options = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
            MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
        };

        private static List<ProductCreated> ProductCreatedEvents => new()
        {
            new ProductCreated(
                Guid.Parse("379CE2B5-6DC2-410C-BB18-51CA84E57162"),
                "Product 1",
                100,
                "images/product1",
                "description 1",
                DateTime.UtcNow),
            new ProductCreated(
                Guid.Parse("651A3A24-4CD3-4E4B-9E00-5B3229D9AAD5"),
                "Product 2",
                200,
                "images/product2",
                "description 2",
                DateTime.UtcNow),
            new ProductCreated(
                Guid.Parse("938D7342-0BE7-4465-A434-1EAE6ACA2FC7"),
                "Product 3",
                300,
                "images/product3",
                "description 3",
                DateTime.UtcNow),
        };

        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                using (var eventStoreContext = new EventStore(scope.ServiceProvider.GetRequiredService<DbContextOptions<EventStore>>()))
                {
                    eventStoreContext.ProductEvents.AddRange(ProductCreatedEvents.Select(e =>
                        new EventEntity<Guid>
                        {
                            AggregateId = e.AggregateId,
                            AggregateVersion = e.AggregateVersion,
                            Timestamp = e.Timestamp,
                            Serialized = JsonConvert.SerializeObject(e, Options)
                        }));

                    eventStoreContext.SaveChanges();
                }

                using (var readModelContext = new ProductsReadDbContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<ProductsReadDbContext>>()))
                {
                    readModelContext.Products.AddRange(ProductCreatedEvents.Select(e =>
                        new ReadModel.Product
                        {
                            Id = e.AggregateId,
                            Name = e.Name,
                            Price = e.Price,
                            Description = e.Description,
                            ImageUri = e.ImageUri
                        }));

                    readModelContext.SaveChanges();
                }
            }
        }
    }
}
