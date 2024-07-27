using System.Windows;
using System.Windows.Controls;

namespace Shop.Presentation.UserControls
{
    public partial class ShopProductRowUC : UserControl
    {
        public static DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProdName", typeof(string), typeof(ShopProductRowUC), new PropertyMetadata(""));
        public string ProdName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }

        public static DependencyProperty ProductQuantityProperty =
            DependencyProperty.Register("ProdQty", typeof(int), typeof(ShopProductRowUC), new PropertyMetadata(-1));
        public int ProdQty
        {
            get { return (int)GetValue(ProductQuantityProperty); }
            set { SetValue(ProductQuantityProperty, value); }
        }

        public ShopProductRowUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
