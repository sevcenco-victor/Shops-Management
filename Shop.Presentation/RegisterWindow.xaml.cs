using Shop.Applications;
using Shop.Domain.Models;
using Shop.Infrastructure;
using System.Windows;
using Shop.Infrastructure.Repositories;
using System.Windows.Forms;
using Azure.Identity;
using Shop.ApplicationServices.Services;

namespace Shop.Presentation
{
    public partial class RegisterWindow : Window
    {
        private byte[]? userImageData = [];
        private readonly UserRepository _userRepository = new UserRepository(new NexusDbContext());
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
        private void Regiser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName.Text))
                    throw new ArgumentException("Name is required.");
                if (string.IsNullOrWhiteSpace(userEmail.Text) || !AppServices.IsValidEmail(userEmail.Text))
                    throw new ArgumentException("A valid email is required.");
                if (string.IsNullOrWhiteSpace(userRole.Text))
                    throw new ArgumentException("Role is required.");
                if (string.IsNullOrWhiteSpace(userPassowrd.Password) || !UserServices.IsValidPassword(userPassowrd.Password))
                    throw new ArgumentException("Password must be at least 8 characters long and contain at least three of the following: uppercase letter, lowercase letter, digit, and special character.");
                if (!userPassowrd.Password.Equals(userRepeatPassowrd.Password))
                    throw new ArgumentException("Passwords must be identical");
                
                UserEntity userEntity = new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Image = userImageData,
                    Name = userName.Text,
                    Email = userEmail.Text,
                    Role = userRole.Text,
                    Password = UserServices.GetHashedPassword(userPassowrd.Password),
                };

                _userRepository.AddUser(userEntity);
                System.Windows.MessageBox.Show("Successfully registered, \nWelcome!");

                MainWindow mainWindow = new MainWindow();
                if (userEntity is not null)
                {
                    mainWindow.SetCurrentUser(userEntity);
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (ArgumentException ex)
            {
                System.Windows.MessageBox.Show($"Input error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show($"Operation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }
        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() is System.Windows.Forms.DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                userImageData = ImageToByteConverter.ConvertImageToByteArray(selectedImagePath);
            }
        }
    }
}
