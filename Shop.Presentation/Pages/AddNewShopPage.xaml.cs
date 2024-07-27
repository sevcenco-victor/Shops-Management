using Shop.Applications;
using Shop.ApplicationServices.Services;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using Shop.Presentation.UserControls;
using System.CodeDom;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Shop.Presentation.Pages
{
    public partial class AddNewShopPage : Page
    {
        private readonly ShopRepository _shopRepository = new ShopRepository(new NexusDbContext());
        private byte[]? selectedImageData;
        public AddNewShopPage()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedImageData is null) throw new ArgumentException("Shop must have an image");

                if (string.IsNullOrEmpty(shopName.Text))
                    throw new ArgumentException("Shop name could not be empty");
                if (string.IsNullOrEmpty(shopAddress.Text))
                    throw new ArgumentException("Shop address could not be empty");
                if (string.IsNullOrEmpty(shopPhone.Text))
                    throw new ArgumentException("Shop phone must be only numbers and not empty");
                if (string.IsNullOrEmpty(shopEmail.Text) || !AppServices.IsValidEmail(shopEmail.Text))
                    throw new ArgumentException("Shop email must be valid");
                if (string.IsNullOrEmpty(shopCity.Text))
                    throw new ArgumentException("Shop city could not be empty");
                if (string.IsNullOrEmpty(shopCountry.Text))
                    throw new ArgumentException("Shop country could not be empty");

                ShopEntity newShop = new ShopEntity()
                {
                    Id = Guid.NewGuid(),
                    Image = selectedImageData,
                    Name = shopName.Text,
                    Address = shopAddress.Text,
                    Phone = shopPhone.Text,
                    Email = shopEmail.Text,
                    City = shopCity.Text,
                    Country = shopCountry.Text,
                };
                _shopRepository.AddShop(newShop);
                System.Windows.MessageBox.Show("Added Successfully!");
            }
            catch (ArgumentException ex)
            {
                System.Windows.MessageBox.Show($"Input error: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            selectedImageData = [];
            shopName.Text = String.Empty;
            shopAddress.Text = String.Empty;
            shopPhone.Text = String.Empty;
            shopEmail.Text = String.Empty;
            shopCity.Text = String.Empty;
            shopCountry.Text = String.Empty;
        }
        private void AddShopImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() is DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                selectedImageData = ImageToByteConverter.ConvertImageToByteArray(selectedImagePath);
            }
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void NumericTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            if (textBox == null)
                return;

            var text = textBox.Text;
            if (!IsTextAllowed(text))
            {
                textBox.Text = RemoveInvalidCharacters(text);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex(@"^[0-9+\-\s\(\)]*$");
            return regex.IsMatch(text);
        }

        private static string RemoveInvalidCharacters(string text)
        {
            var regex = new Regex(@"[^0-9+\-\s\(\)]");
            return regex.Replace(text, string.Empty);
        }

    }
}
