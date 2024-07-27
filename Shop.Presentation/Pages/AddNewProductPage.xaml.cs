using DocumentFormat.OpenXml.Presentation;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using Shop.Presentation.UserControls;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Shop.Presentation.Pages
{
    public partial class AddNewProductPage : Page
    {
        private readonly NexusDbContext _context = new NexusDbContext();
        private readonly ProductRepository _productRepository;
        private readonly StockRepository _stockRepository;
        private readonly Guid _shopId;
        public AddNewProductPage( Guid selectedShopId)
        {
            InitializeComponent();
            _productRepository = new ProductRepository(_context);
            _stockRepository = new StockRepository(_context);
            _shopId = selectedShopId;

            productCategory.ItemsSource = _context.Categories.ToList();
            productCategory.DisplayMemberPath = "Name";

        }

        private void Submit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedCategory = (CategoryEntity)productCategory.SelectedItem;

            ProductEntity newProduct = new ProductEntity()
            {
                Name = productName.Text,
                Description = productDesc.Text,
                Price = double.Parse(productPrice.Text),
                AgeRestrict = short.Parse(productAgeRestrict.Text),
                Country = productCountry.Text,
                CategoryId = selectedCategory.Id,
            };

            Guid existingProductId = _productRepository.ProductExists(newProduct);

            if (existingProductId != Guid.Empty)
            {
                newProduct.Id = existingProductId;

                var shopStock = _stockRepository.GetProductsAndStocksByShopId(_shopId);
                foreach (var stock in shopStock)
                {
                    if (stock.Product.Name.ToLower() == newProduct.Name.ToLower())
                    {
                        MessageBox.Show("Shop already contains this Product", "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
                AddNewStock(existingProductId);
            }
            else
            {
                _productRepository.AddProduct(newProduct);
                AddNewStock(newProduct.Id);
            }
            MessageBox.Show("You have Successfully added product!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);

        }
        private void AddNewStock(Guid productId)
        {
            StockEntity newStock = new StockEntity()
            {
                ShopId = _shopId,
                ProductId = productId,
                Quantity = int.Parse(productQuantity.Text),
            };
            _stockRepository.AddStock(newStock);
        }
        private void Reset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            productName.Text = "";
            productDesc.Text = "";
            productPrice.Text = "";
            productAgeRestrict.Text = "";
            productCountry.Text = "";
            productCategory.SelectedValue = "";
            productQuantity.Text = "";
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void NumericTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
                return;

            var text = textBox.Text;
            var selectionStart = textBox.SelectionStart;
            var selectionLength = textBox.SelectionLength;

            var newText = RemoveInvalidCharacters(text);
            if (text != newText)
            {
                textBox.Text = newText;
                textBox.SelectionStart = selectionStart > newText.Length ? newText.Length : selectionStart;
                textBox.SelectionLength = selectionLength;
            }
        }
        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[0-9]*\.?[0-9]*$");
            return regex.IsMatch(text);
        }

        private static string RemoveInvalidCharacters(string text)
        {
            var regex = new Regex("[^0-9.]");
            return regex.Replace(text, string.Empty);
        }
       
    }
}
