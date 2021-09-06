using JKang.EventSourcing.Persistence.EfCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alza.Product.Infrastructure
{
    public class EventStore : DbContext, IEventDbContext<Domain.Product, Guid>
    {
        public DbSet<EventEntity<Guid>> ProductEvents { get; set; }

        public EventStore(DbContextOptions<EventStore> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EventEntityConfiguration<Guid>());
        }

        DbSet<EventEntity<Guid>> IEventDbContext<Domain.Product, Guid>.GetEventDbSet() => ProductEvents;
    }
}
