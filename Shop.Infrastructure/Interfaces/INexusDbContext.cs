using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;

namespace Shop.Infrastructure
{
    public  interface INexusDbContext
    {
        public DbSet<ShopEntity> Shops { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<StockEntity> Stocks { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
    }
}