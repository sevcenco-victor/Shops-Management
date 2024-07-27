using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shop.Presentation.UserControls
{
    public partial class PageHeaderUC : UserControl
    {
        public static DependencyProperty PageNameProperty =
            DependencyProperty.Register("PageName", typeof(string), typeof(PageHeaderUC), new PropertyMetadata(""));

        public string PageName
        {
            get { return (string)GetValue(PageNameProperty); }
            set { SetValue(PageNameProperty, value); }
        }

        public static readonly DependencyProperty UserIconProperty =
    DependencyProperty.Register("UserIcon", typeof(ImageSource), typeof(PageHeaderUC), new PropertyMetadata(null));

        public ImageSource UserIcon
        {
            get { return (ImageSource)GetValue(UserIconProperty); }
            set { SetValue(UserIconProperty, value); }
        }


        public static DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(PageHeaderUC), new PropertyMetadata(""));

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static DependencyProperty UserEmailProperty =
            DependencyProperty.Register("UserEmail", typeof(string), typeof(PageHeaderUC), new PropertyMetadata(""));

        public string UserEmail
        {
            get { return (string)GetValue(UserEmailProperty); }
            set { SetValue(UserEmailProperty, value); }
        }

        public PageHeaderUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Icons/searchactive.png"));
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            searchIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Icons/search.png"));
        }

        public event TextChangedEventHandler SearchTextChanged;

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTextChanged?.Invoke(sender, e);
        }

    }
}
