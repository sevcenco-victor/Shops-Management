using Shop.Applications;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using Shop.Presentation.UserControls;
using System.Windows.Controls;

namespace Shop.Presentation.Pages
{
    public partial class ShopDetailsPage : Page
    {
        private readonly StockRepository _stockRepository = new StockRepository(new NexusDbContext());
        private readonly CategoryRepository _categoryRepository = new CategoryRepository(new NexusDbContext());
        private readonly ProductRepository _productRepository = new ProductRepository(new NexusDbContext());

        private readonly ShopEntity _shopEntity;
        private IEnumerable<(ProductEntity product, StockEntity stock)> _productStock = [];
        public ShopDetailsPage()
        {
            InitializeComponent();
            InitPageContent();
            InitShopProducts();
        }
        public ShopDetailsPage(ShopEntity selectedShop)
        {
            InitializeComponent();
            _shopEntity = selectedShop;
            _productStock = _stockRepository.GetProductsAndStocksByShopId(_shopEntity.Id);

            InitPageContent();
            InitShopProducts();
            InitFoodProducts();

            ProductEntity cheapestProduct = GetCheapestProduct();
            if (cheapestProduct is not null)
            {
                ProductPrintRowUC productRow = new ProductPrintRowUC()
                {
                    ProdName = cheapestProduct.Name,
                    ProdDescription = cheapestProduct.Description,
                    ProdPrice = cheapestProduct.Price.ToString(),
                    ProdAgeRestrict = cheapestProduct.AgeRestrict.ToString(),
                    ProdCountry = cheapestProduct.Country,
                    Category = _categoryRepository.GetCategoryById(cheapestProduct.CategoryId).Name,
                };
                cheapProduct.Children.Add(productRow);
            }

            ProductEntity mostExpensiveProduct = GetTheMostExpensiveProduct();
            if (mostExpensiveProduct is not null)
            {
                ProductPrintRowUC productRow1 = new ProductPrintRowUC()
                {
                    ProdName = mostExpensiveProduct.Name,
                    ProdDescription = mostExpensiveProduct.Description,
                    ProdPrice = mostExpensiveProduct.Price.ToString(),
                    ProdAgeRestrict = mostExpensiveProduct.AgeRestrict.ToString(),
                    ProdCountry = mostExpensiveProduct.Country,
                    Category = _categoryRepository.GetCategoryById(mostExpensiveProduct.CategoryId).Name,
                };
                expensiveProduct.Children.Add(productRow1);
            }
        }
        private ProductEntity GetCheapestProduct()
        {
            ProductEntity cheapestProduct = _productStock.FirstOrDefault().product;

            foreach (var item in _productStock)
            {
                if (item.product.Price < cheapestProduct.Price)
                {
                    cheapestProduct = item.product;
                }
            }
            return cheapestProduct;
        }
        private ProductEntity GetTheMostExpensiveProduct()
        {
            ProductEntity theMostExpensivePorduct = _productStock.FirstOrDefault().product;

            foreach (var item in _productStock)
            {
                if (item.product.Price > theMostExpensivePorduct.Price)
                {
                    theMostExpensivePorduct = item.product;
                }
            }
            return theMostExpensivePorduct;
        }
        private void InitFoodProducts()
        {
            var sortedProducts = _productStock.OrderBy(p => p.product.Name);

            foreach (var item in sortedProducts)
            {
                CategoryEntity? productCategory = _categoryRepository.GetCategoryById(item.product.CategoryId);
                if (productCategory != null && productCategory.Name.ToLower().Contains("food"))
                {
                    ShopProductRowUC shopProduct = new ShopProductRowUC()
                    {
                        ProdName = item.product.Name,
                        ProdQty = item.stock.Quantity,
                    };

                    shopFoodProductsTable.Children.Add(shopProduct);
                }
            }
        }
        private void InitShopProducts()
        {
            foreach (var item in _productStock)
            {
                ShopProductRowUC shopProduct = new ShopProductRowUC()
                {
                    ProdName = item.product.Name,
                    ProdQty = item.stock.Quantity,
                };

                shopProductsTable.Children.Add(shopProduct);
            }
        }
        private void InitPageContent()
        {
            shopImage.Source = ImageToByteConverter.ConvertByteArrayToImage(_shopEntity.Image);
            shopName.Text = _shopEntity.Name;
            shopAddress.Text = _shopEntity.Address;
            shopPhone.Text = _shopEntity.Phone;
            shopEmail.Text = _shopEntity.Email;
            shopCityCountry.Text = _shopEntity.City + " / " + _shopEntity.Country;
        }

        private void AddNewProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.Instance.mainFrame.Navigate(new AddNewProductPage( _shopEntity.Id));
        }
    }
}
