using Shop.Applications;
using Shop.Domain.Models;
using Shop.Infrastructure.Repositories;
using Shop.Presentation.Pages;
using Shop.Presentation.UserControls;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shop.Presentation
{
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }
        public static PageHeaderUC? pageHeaderUC { get; private set; }
        public static UserEntity? currentUser { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;

            new StockRepository(new Infrastructure.NexusDbContext()).DeleteStocksWithZeroQuantity();

            //SeedData.ClientSeedData();
            //SeedData.CategoriesSeedData();
            // SeedData.ProductSeedData();
            // SeedData.StockSeedData();
            // SeedData.OrderSeedData();
            //SeedData.OrderThisMonthData();
            //SeedData.AddNewOrders(80, DateTime.Now);
            //SeedData.AddNewOrders(156, DateTime.Now.AddDays(-1));
        }
        private MenuButtonUC? lastButton = null;
        public void SetCurrentUser(UserEntity user)
        {
            currentUser = user;
            InitPageHeader();
        }

        private void InitPageHeader()
        {
            pageHeaderUC = new PageHeaderUC()
            {
                PageName = "PageName",
                UserIcon = ImageToByteConverter.ConvertByteArrayToImage(currentUser.Image),
                UserName = currentUser.Name,
                UserEmail = currentUser.Email,
            };
            Grid.SetRow(pageHeaderUC, 0);
            pageContent.Children.Add(pageHeaderUC);
        }
        public static void SetPageHeaderName(string pageName)
        {
            pageHeaderUC.PageName = pageName;
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuButtonUC? button = sender as MenuButtonUC;
            if (button != null)
            {
                if (lastButton != null)
                {
                    RestoreButtonAppearance(lastButton);
                }
                ChangeButtonAppearance(button);
                lastButton = button;

                string? buttonTag = button.Tag as string;
                if (!string.IsNullOrEmpty(buttonTag))
                {
                    switch (buttonTag)
                    {
                        case "dashboard":
                            mainFrame.Navigate(new DashboardPage());
                            break;
                        case "shop":
                            mainFrame.Navigate(new ShopsPage());
                            break;
                        case "logout":
                            LogoutButton_Click();
                            break;
                    }
                }
            }
        }
        private void LogoutButton_Click()
        {
            string directoryPath = "user-info";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine("user-info", "credentials.txt");

            if (File.Exists(filePath))
                File.Delete(filePath);

            new LoginWindow().Show();
            this.Close();
        }
        private void RestoreButtonAppearance(MenuButtonUC button)
        {
            button.buttonText.Foreground = (Brush)FindResource("gray");
            button.ImagePath = "/Assets/Icons/" + GetImageName(button.ImagePath) + ".png";
        }

        private void ChangeButtonAppearance(MenuButtonUC button)
        {
            button.buttonText.Foreground = (Brush)FindResource("aqua");
            button.ImagePath = "/Assets/Icons/" + GetImageName(button.ImagePath) + "Active.png";
        }
        private static string GetImageName(string imagePath)
        {
            string imageName = Path.GetFileNameWithoutExtension(imagePath);

            if (imageName.EndsWith("Active"))
            {
                imageName = imageName.Substring(0, imageName.Length - "Active".Length).Trim();
            }

            return imageName;
        }

        private void OfferButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new HelpPage());
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DashboardMenuButon.Button_Click(DashboardMenuButon, new RoutedEventArgs());
        }
    }
}