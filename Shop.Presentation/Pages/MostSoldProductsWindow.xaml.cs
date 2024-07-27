using Shop.ApplicationServices.Services;
using System.Windows;

namespace Shop.Presentation.Pages
{
    public partial class MostSoldProductsWindow : Window
    {
        public MostSoldProductsWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = ProductServices.GetPopularProducts(10);
        }
    }
}
