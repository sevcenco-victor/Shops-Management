using System.Windows;
using System.Windows.Controls;

namespace Shop.Presentation.UserControls
{
    public partial class ProductPrintRowUC : UserControl
    {
        public static DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProdName", typeof(string), typeof(ProductPrintRowUC), new PropertyMetadata(""));
        public string ProdName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }

        public static DependencyProperty ProductDescriptionProperty =
            DependencyProperty.Register("ProdDescription", typeof(string), typeof(ProductPrintRowUC), new PropertyMetadata(""));
        public string ProdDescription
        {
            get { return (string)GetValue(ProductDescriptionProperty); }
            set { SetValue(ProductDescriptionProperty, value); }
        }

        public static DependencyProperty ProductPriceProperty =
            DependencyProperty.Register("ProdPrice", typeof(string), typeof(ProductPrintRowUC), new PropertyMetadata(""));
        public string ProdPrice
        {
            get { return (string)GetValue(ProductPriceProperty); }
            set { SetValue(ProductPriceProperty, value); }
        }
        public static DependencyProperty ProductAgeRestrictProperty =
            DependencyProperty.Register("ProdAgeRestrict", typeof(string), typeof(ProductPrintRowUC), new PropertyMetadata(""));
        public string ProdAgeRestrict
        {
            get { return (string)GetValue(ProductAgeRestrictProperty); }
            set { SetValue(ProductAgeRestrictProperty, value); }
        }
        public static DependencyProperty ProductCountryProperty =
            DependencyProperty.Register("ProdCountry", typeof(string), typeof(ProductPrintRowUC), new PropertyMetadata(""));
        public string ProdCountry
        {
            get { return (string)GetValue(ProductCountryProperty); }
            set { SetValue(ProductCountryProperty, value); }
        }

        public static DependencyProperty ProductCategoryIDProperty =
           DependencyProperty.Register("ProdCategory", typeof(string), typeof(ProductPrintRowUC), new PropertyMetadata(""));
        public string Category
        {
            get { return (string)GetValue(ProductCategoryIDProperty); }
            set { SetValue(ProductCategoryIDProperty, value); }
        }


        public ProductPrintRowUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
