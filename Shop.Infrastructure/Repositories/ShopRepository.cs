using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using System;

namespace Shop.Infrastructure.Repositories
{
    public class ShopRepository
    {
        private readonly NexusDbContext _context;
        private readonly Random random = new Random();

        public ShopRepository(NexusDbContext context)
        {
            _context = context;
        }

        public void AddShop(ShopEntity shop)
        {
            _context.Shops.Add(shop);
            _context.SaveChanges();
        }

        public IEnumerable<ShopEntity> GetAllShops()
        {
            return _context.Shops.ToList();
        }

        public ShopEntity? GetShopById(Guid id)
        {
            return _context.Shops
                .FirstOrDefault(s => s.Id == id);
        }


        public void UpdateShop(Guid id, string name, string address, string city, string country)
        {
            _context.Shops
               .Where(s => s.Id == id)
               .ExecuteUpdate(s => s
                   .SetProperty(c => c.Name, name)
                   .SetProperty(c => c.Address, address)
                   .SetProperty(c => c.City, city)
                   .SetProperty(c => c.Country, country));
        }
        public ShopEntity? GetRandomShop()
        {
            int index = random.Next(0, _context.Shops.Count());
            return _context.Shops.Skip(index).FirstOrDefault();
        }
        public void DeleteShop(Guid id)
        {
            _context.Shops
                   .Where(s => s.Id == id)
                   .ExecuteDelete();
        }

    }
}
