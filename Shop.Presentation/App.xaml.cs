using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.ApplicationServices.Services;
using Shop.Domain.Models;
using Shop.Infrastructure;
using System.IO;
using System.Windows;

namespace Shop.Presentation
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        private const string connectionString = "Data Source=RITZU;Integrated Security=True;Database=NexsusDb;Trust Server Certificate=True";
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<NexusDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddSingleton<MainWindow>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (File.Exists("user-info/credentials.txt"))
            {
                byte[] encryptedBytes = File.ReadAllBytes("user-info/credentials.txt");
                string decryptedData = EncryptService.DecryptData(encryptedBytes);
                string[] decryptedParts = decryptedData.Split('|');
                string storedEmail = decryptedParts[0];
                string storedPassword = decryptedParts[1];

                var user = new UserServices().AuthenticateUser(storedEmail, storedPassword);

                if (user != null)
                    GrantAccess(user);
                else
                    new LoginWindow().Show();
            }
            else
            {
                new LoginWindow().Show();
            }
        }
        public static void GrantAccess(UserEntity validUser)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.SetCurrentUser(validUser);
            mainWindow.Show();
        }
    }

}
