using Bogus;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
namespace Shop.ApplicationServices.Services
{
    public class ProductServices
    {
        private static readonly NexusDbContext _context = new NexusDbContext();
        public static ProductEntity GenerateRandomProduct(CategoryRepository categoryRepository)
        {
            var faker = new Faker<ProductEntity>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductAdjective())
                .RuleFor(p => p.Price, f => Math.Round(f.Random.Double(1, 100), 2))
                .RuleFor(p => p.AgeRestrict, f => f.Random.Short(0, 18))
                .RuleFor(p => p.Country, f => f.Address.Country())
                .RuleFor(p => p.CategoryId, categoryRepository.GetRandomCategory().Id)
                .RuleFor(p => p.Category, (f, p) =>
                {
                    var categoryId = p.CategoryId;
                    return categoryRepository.GetCategoryById(categoryId);
                });

            return faker.Generate();
        }
        public static List<ProductEntity?> GetPopularProducts(int firstNProducts)
        {
            var popularProducts = _context.Orders
                .GroupBy(order => order.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(order => order.Quantity)
                })
                .OrderByDescending(item => item.TotalQuantity)
                .Take(firstNProducts)
                .ToList();

            return popularProducts
                .Select(item => _context.Products.FirstOrDefault(p => p.Id == item.ProductId))
                .ToList();
        }
        
    }
}
