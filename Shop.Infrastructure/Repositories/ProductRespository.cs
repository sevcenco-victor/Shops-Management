using Shop.Domain.Models;

namespace Shop.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private readonly NexusDbContext _context;

        public ProductRepository(NexusDbContext context)
        {
            _context = context;
        }

        public void AddProduct(ProductEntity product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public ProductEntity? GetProductById(Guid id)
        {
            return _context.Products
                .FirstOrDefault(p => p.Id.Equals(id));
        }
        public void UpdateProduct(Guid id, string name, string description, double price, short ageRestrict, Guid categoryId)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.AgeRestrict = ageRestrict;
                product.CategoryId = categoryId;

                _context.SaveChanges();
            }
        }
        public ProductEntity? GetRandomProduct()
        {
            Random random = new Random();
            int index = random.Next(0, _context.Products.Count());
            return _context.Products.Skip(index).FirstOrDefault();
        }
        public Guid ProductExists(ProductEntity product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Name == product.Name && p.Country == product.Country);

            if (existingProduct != null) return existingProduct.Id;
            return Guid.Empty;
        }
        public void DeleteProduct(Guid id)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
