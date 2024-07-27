using DocumentFormat.OpenXml.Bibliography;
using Shop.Applications;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using Shop.Presentation.UserControls;
using Shop.Presentation.UserControls.Cards;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Shop.Presentation.Pages
{
    public partial class ShopsPage : Page
    {
        private readonly ShopRepository _shopRepository = new ShopRepository(new NexusDbContext());
        //private readonly PageHeaderUC _pageHeader =  MainWindow.pageHeaderUC;
        private IEnumerable<ShopEntity> shopList;
        private IEnumerable<ShopCardUC> shopCardList;

        public ShopsPage()
        {
            shopList = _shopRepository.GetAllShops();
 
            InitializeComponent();
            shopCardList = InitShopCardsList();
            InitPageContent();

            MainWindow.SetPageHeaderName("Shops");
            MainWindow.pageHeaderUC.SearchTextChanged += PageHeader_SearchTextChanged;
            this.Unloaded += ShopsPage_Unloaded;
        }
        private void ShopsPage_Unloaded(object sender, RoutedEventArgs e)
        {
            var pageHeader = MainWindow.pageHeaderUC;
            if (pageHeader != null)
            {
                pageHeader.SearchTextChanged -= PageHeader_SearchTextChanged;
            }
        }
        private IEnumerable<ShopCardUC> InitShopCardsList()
        {
            foreach(var shop in shopList)
            {
                var newShopCard = new ShopCardUC(shop)
                {
                    ShopName = shop.Name,
                    Image = ImageToByteConverter.ConvertByteArrayToImage(shop.Image),
                    Address = shop.Address,
                    Email = shop.Email,
                    Phone = shop.Phone
                };
                newShopCard.Margin = new Thickness(24, 0, 0, 24);
                yield return newShopCard;
            }
        }
        private void PageHeader_SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string searchText = textBox.Text.ToLower();
                foreach (var child in pageWrapper.Children)
                {
                    if (child is ShopCardUC shopCard)
                    {
                        if (shopCard.ShopName.ToLower().Contains(searchText))
                        {
                            shopCard.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            shopCard.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }
        private void InitPageContent()
        {
            pageWrapper.Children.Clear();
            foreach (var shopCard in shopCardList) 
                pageWrapper.Children.Add(shopCard);
        }

        private void OnClick(object page)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);

            while (parent != null && (parent is not MainWindow))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent != null)
            {
                MainWindow? mainWindow = parent as MainWindow;
                mainWindow?.mainFrame.Navigate(page);
            }
        }
        private void AddNewShop_Click(object sender, RoutedEventArgs e)
        {
            OnClick(new AddNewShopPage());
        }
        private void GetProductAvg_Click(object sender, RoutedEventArgs e)
        {
            new ProductAveragePriceWindow().Show();
        }
        private void ProductByCountry_Click(object sender, RoutedEventArgs e)
        {
            new ProductsByCountryWindow().Show();
        }
        private void ChildProducts_Click(object sender, RoutedEventArgs e)
        {
            new ChildrenProductsWindow().Show();
        }
        private void ExportToys_Click(object sender, RoutedEventArgs e)
        {
            new ExportToysWindow().Show();
        }
        private void TopSoldProd_Click(object sender, RoutedEventArgs e)
        {
            new MostSoldProductsWindow().Show();
        }
        private void FindProduct_Click(object sender, RoutedEventArgs e)
        {
            new FindProductWindow().Show();
        }
    }
}