using Microsoft.EntityFrameworkCore;

namespace Alza.Product.Infrastructure.ReadModels
{
    public class ProductsReadDbContext : DbContext
    {
        public DbSet<ReadModel.Product> Products { get; set; }

        public ProductsReadDbContext(DbContextOptions<ProductsReadDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ReadModel.Product>()
                .HasKey(e => e.Id);
        }
    }
}
