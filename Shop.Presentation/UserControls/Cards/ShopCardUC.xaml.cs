using Shop.Domain.Models;
using Shop.Presentation.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shop.Presentation.UserControls.Cards
{
    public partial class ShopCardUC : UserControl
    {
        private ShopEntity _shopEntity;

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ShopCardUC), new PropertyMetadata(null));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ShopNameProperty =
            DependencyProperty.Register("ShopName", typeof(string), typeof(ShopCardUC), new PropertyMetadata(""));

        public string ShopName
        {
            get { return (string)GetValue(ShopNameProperty); }
            set { SetValue(ShopNameProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(ShopCardUC), new PropertyMetadata(""));

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(ShopCardUC), new PropertyMetadata(""));

        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }

        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(ShopCardUC), new PropertyMetadata(""));

        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        public ShopCardUC(ShopEntity shopEntity)
        {
            InitializeComponent();
            DataContext = this;
            _shopEntity = shopEntity;
        }

        private void Shop_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow.Instance.mainFrame.Navigate(new ShopDetailsPage(_shopEntity));
        }
    }
}
