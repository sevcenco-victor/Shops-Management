using Shop.ApplicationServices.Services;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Shop.Presentation
{
    public partial class LoginWindow : Window
    {
        private UserRepository _userRepository = new UserRepository(new NexusDbContext());
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var validUser = new UserServices().AuthenticateUser(userEmail.Text, userPassword.Password);
            if (validUser != null)
            {
                string directoryPath = "user-info";
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine("user-info", "credentials.txt");

                if (remeberCheck.IsChecked == true)
                {
                    byte[] encryptedData = EncryptService.EncryptData(userEmail.Text + "|" + userPassword.Password);
                    File.WriteAllBytes(filePath, encryptedData);
                }
                App.GrantAccess(validUser);
                this.Close();
            }
            else
                MessageBox.Show("Incorrect name or password! \nTry one more time or click Forgot Password under the password field", "Wrong!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Register_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new RegisterWindow().Show();
            this.Close();
        }

        private void ForgotPass_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new ForgotPassWindow().Show();
            this.Close();
        }
    }
}
