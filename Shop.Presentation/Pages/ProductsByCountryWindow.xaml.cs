using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using Shop.Presentation.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Shop.Presentation.Pages
{
    public partial class ProductsByCountryWindow : Window
    {
        private readonly NexusDbContext _context = new NexusDbContext();
        private readonly CategoryRepository _categoryRespository;
        private readonly List<ProductEntity> _products = [];
        public ProductsByCountryWindow()
        {
            InitializeComponent();
            _categoryRespository= new CategoryRepository(_context);
            _products = _context.Products.ToList();

            countryComboBox.ItemsSource = GetUniqueCountries();
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productsDataGrid.ItemsSource = GetFilteredProducts(countryComboBox.SelectedItem.ToString());
        }
        private IEnumerable<ProductEntity> GetFilteredProducts(string country)
        {
            foreach (var product in _products)
            {
                if (product.Country == country)
                {
                    product.Category = _categoryRespository.GetCategoryById(product.CategoryId);
                    yield return product;
                }
            }
        }
        private List<string> GetUniqueCountries()
        {
            var countries = _products.Select(p => p.Country).ToList();

            return countries.Distinct().ToList();
        }

    }
}
