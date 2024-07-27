using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace Shop.Presentation.Pages
{
    public partial class FindProductWindow : Window
    {
        private readonly NexusDbContext _context = new NexusDbContext();
        private readonly List<ShopEntity> shops;
        private readonly List<(ShopEntity Shop, IEnumerable<ProductEntity?> products)> shopProducts
                = new List<(ShopEntity Shop, IEnumerable<ProductEntity?> products)>();
        private readonly ShopRepository _shopRespository;
        private readonly StockRepository _stockRespository;
        public FindProductWindow()
        {
            _shopRespository = new ShopRepository(_context);
            _stockRespository = new StockRepository(_context);
            shops = _context.Shops.ToList();
            InitShopProducts();
            InitializeComponent();
        }
        private void InitShopProducts()
        {
            foreach (var shop in shops)
            {
                shopProducts.Add((shop, _stockRespository.GetProductsByShopId(shop.Id)));
            }
        }

        private void productName_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ShopEntity> filteredShops = new List<ShopEntity>();
            foreach (var shop in shopProducts)
            {
                foreach (var product in shop.products)
                {
                    if (product.Name.ToLower().Contains(productName.Text.ToLower()))
                    {
                        filteredShops.Add(shop.Shop);
                        break;
                    }
                }
            }
            dataGrid.ItemsSource = filteredShops;
        }
    }
}
