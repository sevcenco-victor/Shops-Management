
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using Shop.Infrastructure.Configurations;

namespace Shop.Infrastructure
{

    public class NexusDbContext : DbContext, INexusDbContext
    {
        private readonly string connectionString = "Data Source=RITZU;Integrated Security=True;Database=NexsusDb;Trust Server Certificate=True";
        public NexusDbContext() : base()
        {
        }
        public NexusDbContext(DbContextOptions<NexusDbContext> options) : base(options)
        {
        }
        public DbSet<ShopEntity> Shops { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<StockEntity> Stocks { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShopConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
