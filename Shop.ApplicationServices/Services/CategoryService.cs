using Bogus;
using Shop.Domain.Models;

namespace Shop.ApplicationServices.Services
{
    public static class CategoryService
    {
        private static readonly List<string> UsedCategoryNames = new List<string>();

        public static CategoryEntity GenerateRandomCategory()
        {
            var faker = new Faker<CategoryEntity>()
                .CustomInstantiator(f => new CategoryEntity { Name = GenerateUniqueCategoryName() });

            return faker.Generate();
        }

        private static string GenerateUniqueCategoryName()
        {
            var faker = new Faker();
            string categoryName;
            do
            {
                categoryName = faker.Commerce.Categories(1)[0];
            } while (UsedCategoryNames.Contains(categoryName));

            UsedCategoryNames.Add(categoryName);
            return categoryName;
        }
    }
}
