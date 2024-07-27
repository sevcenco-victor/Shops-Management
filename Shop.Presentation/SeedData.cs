using Microsoft.EntityFrameworkCore;
using Shop.ApplicationServices.Services;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;

namespace Shop.Presentation
{
    public static class SeedData
    {
        private static readonly NexusDbContext _context = new NexusDbContext();
        private static readonly ShopRepository _shopRespository = new ShopRepository(_context);
        private static readonly ProductRepository _productRepository = new ProductRepository(_context);
        private static readonly CategoryRepository _categoryRepository = new CategoryRepository(_context);
        private static readonly ClientRepository _clientRepository = new ClientRepository(_context);

        private static readonly Random random = new Random();
        public static void ClientSeedData()
        {
            int numOfClients = 20;
            List<ClientEntity> clients = ClientService.GenerateRandomClient().Generate(numOfClients);

            _context.Clients.AddRange(clients);
            _context.SaveChanges();
        }

        public static void CategoriesSeedData()
        {
            List<CategoryEntity> categories = new List<CategoryEntity>();
            for (int i = 0; i < 16; i++)
            {
                categories.Add(CategoryService.GenerateRandomCategory());
            }

            _context.Categories.AddRange(categories);
            _context.SaveChanges();
        }
        public static void ProductSeedData()
        {
            List<ProductEntity> products = new List<ProductEntity>();
            for (int i = 0; i < 500; i++)
            {
                products.Add(ProductServices.GenerateRandomProduct(_categoryRepository));
            }

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
        public static void AddNewOrders(int numOfOrders, DateTime date)
        {
            List<OrderEntity> orders = new List<OrderEntity>();

            for (int i = 0; i < numOfOrders; i++)
            {
                OrderEntity order = new OrderEntity
                {
                    OrderDate = date,
                    Quantity = random.Next(1, 7),
                    ShopId = _shopRespository.GetRandomShop().Id,
                    ProductId = _productRepository.GetRandomProduct().Id,
                    ClientID = _clientRepository.GetRandomClient().Id,
                };
                order.Shop = _shopRespository.GetShopById(order.ShopId);
                order.Product = _productRepository.GetProductById(order.ProductId);
                order.Client = _clientRepository.GetClientById(order.ClientID);

                orders.Add(order);
            }
            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }
        public static void OrderSeedData()
        {
            List<OrderEntity> orders = new List<OrderEntity>();

            DateTime minDate = new DateTime(2024, 4, 1);
            DateTime maxDate = DateTime.Now;

            int range = (maxDate - minDate).Days;
            for (int i = 0; i < 120; i++)
            {
                DateTime randomDate = minDate.AddDays(random.Next(range));
                randomDate = randomDate.AddHours(random.Next(5, 23));


                OrderEntity order = new OrderEntity
                {
                    OrderDate = randomDate,
                    Quantity = random.Next(1, 7),
                    ShopId = _shopRespository.GetRandomShop().Id,
                    ProductId = _productRepository.GetRandomProduct().Id,
                    ClientID = _clientRepository.GetRandomClient().Id,
                };
                order.Shop = _shopRespository.GetShopById(order.ShopId);
                order.Product = _productRepository.GetProductById(order.ProductId);
                order.Client = _clientRepository.GetClientById(order.ClientID);

                orders.Add(order);
            }

            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }

        public static void OrderThisMonthData()
        {
            List<OrderEntity> orders = new List<OrderEntity>();

            DateTime currentDate = DateTime.Now;

            for (int i = 0; i < 240; i++)
            {
                DateTime randomDate = new DateTime(
                    currentDate.Year,
                    currentDate.Month,
                    random.Next(1, DateTime.DaysInMonth(currentDate.Year, currentDate.Month)) + 1
                );
                randomDate = randomDate.AddHours(random.Next(5, 23));

                OrderEntity order = new OrderEntity
                {
                    OrderDate = randomDate,
                    Quantity = random.Next(1, 7),
                    ShopId = _shopRespository.GetRandomShop().Id,
                    ProductId = _productRepository.GetRandomProduct().Id,
                    ClientID = _clientRepository.GetRandomClient().Id,
                };
                order.Shop = _shopRespository.GetShopById(order.ShopId);
                order.Product = _productRepository.GetProductById(order.ProductId);
                order.Client = _clientRepository.GetClientById(order.ClientID);

                orders.Add(order);
            }

            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }
        public static void StockSeedData()
        {
            List<StockEntity> stocks = _context.Stocks.ToList();
            for (int i = 0; i < 1000; i++)
            {
                GenerateNewStock(stocks);
            }

            _context.Stocks.ExecuteDelete();

            _context.Stocks.AddRange(stocks);
            _context.SaveChanges();
        }
        private static void GenerateNewStock(List<StockEntity> stocks)
        {
            StockEntity newStock = new StockEntity()
            {
                Quantity = random.Next(1, 20),
                ShopId = _shopRespository.GetRandomShop().Id,
                ProductId = _productRepository.GetRandomProduct().Id,
            };
            newStock.Shop = _shopRespository.GetShopById(newStock.ShopId);
            newStock.Product = _productRepository.GetProductById(newStock.ProductId);

            StockEntity existingStock = stocks.FirstOrDefault(s => s.ShopId == newStock.ShopId && s.ProductId == newStock.ProductId);

            if (existingStock is not null)
            {
                existingStock.Quantity += newStock.Quantity;
            }
            else
            {
                stocks.Add(newStock);
            }
        }
    }
}
