using Shop.Domain.Models;
using Shop.Infrastructure;
using System.Windows;
using System.Windows.Controls;

namespace Shop.Presentation.Pages
{
    public partial class ProductAveragePriceWindow : Window
    {
        private readonly NexusDbContext _context = new NexusDbContext();
        private readonly List<ProductEntity> productList = [];
        public ProductAveragePriceWindow()
        {
            InitializeComponent();
            productList = _context.Products.ToList();

            productComboBox.ItemsSource = productList;
            productComboBox.DisplayMemberPath = "Name";
        }

        private void productComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (productComboBox.SelectedItem != null)
            {
                ProductEntity selectedProduct = (ProductEntity)productComboBox.SelectedItem;

                productAvgPrice.Text = selectedProduct.Price.ToString()+" $";
            }
            else
            {
                productAvgPrice.Text = "N/A";
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = textBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                productComboBox.ItemsSource = null;
                productComboBox.ItemsSource = productList;
                return;
            }

            productComboBox.ItemsSource = productList.Where(product => product.Name.ToLower().Contains(searchText)).ToList();
            productComboBox.SelectedIndex = 0;
        }

    }
}
