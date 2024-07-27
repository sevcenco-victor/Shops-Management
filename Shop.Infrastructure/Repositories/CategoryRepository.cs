
using Shop.Domain.Models;

namespace Shop.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly NexusDbContext _context;
        private readonly Random random = new Random();

        public CategoryRepository(NexusDbContext context)
        {
            _context = context;
        }

        public void AddCategory(CategoryEntity category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public IEnumerable<CategoryEntity> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public CategoryEntity? GetCategoryById(Guid id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public CategoryEntity? GetRandomCategory()
        {
            int totalCount = _context.Categories.Count();
            int randomIndex = random.Next(0, totalCount);
            return _context.Categories.Skip(randomIndex).FirstOrDefault();
        }

        public void UpdateCategory(CategoryEntity category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            var categoryToDelete = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                _context.SaveChanges();
            }
        }
    }
}
