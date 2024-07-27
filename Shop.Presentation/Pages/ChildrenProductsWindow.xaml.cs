using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Presentation.UserControls;
using System.Windows;

namespace Shop.Presentation.Pages
{
    public partial class ChildrenProductsWindow : Window
    {
        private readonly NexusDbContext _context = new NexusDbContext();
        private readonly List<ProductEntity> _products = [];
        public ChildrenProductsWindow()
        {
            InitializeComponent();
            _products = _context.Products.ToList();
        }

        private void GetProducts_Click(object sender, RoutedEventArgs e)
        {
            productDataGrid.Items.Clear(); // Curățați elementele existente

            double price = double.Parse(filterPrice.Text.Trim());
            int fromAge = int.Parse(filterFromAge.Text.Trim());
            int toAge = int.Parse(filterToAge.Text.Trim());

            List<ProductPrintRowUC> filteredProducts = new List<ProductPrintRowUC>();

            foreach (var product in _products)
            {
                if (product.Price <= price && product.AgeRestrict >= fromAge && product.AgeRestrict <= toAge)
                {
                    filteredProducts.Add(new ProductPrintRowUC()
                    {
                        ProdName = product.Name,
                        ProdAgeRestrict = product.AgeRestrict.ToString(),
                        ProdCountry = product.Country,
                        ProdDescription = product.Description,
                        ProdPrice = product.Price.ToString(),
                    });
                }
            }

            foreach (var product in filteredProducts)
            {
                productDataGrid.Items.Add(product);
            }
        }

    }
}
