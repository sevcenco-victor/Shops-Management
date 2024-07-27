using Shop.Presentation.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shop.Presentation.UserControls
{
    public partial class RevenueBadgeUC : UserControl
    {

        public static readonly DependencyProperty RevenueTypeProperty =
            DependencyProperty.Register("RevenueType", typeof(string), typeof(RevenueBadgeUC), new PropertyMetadata(""));
        public static readonly DependencyProperty BadgeTextProperty =
            DependencyProperty.Register("BadgeText", typeof(string), typeof(RevenueBadgeUC), new PropertyMetadata(""));
        public static readonly DependencyProperty BadgeForegroundProperty =
            DependencyProperty.Register("BadgeForeground", typeof(Brush), typeof(RevenueBadgeUC), new PropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty CurrentRevenueProperty =
        DependencyProperty.Register("CurrentRevenue", typeof(string), typeof(RevenueBadgeUC), new PropertyMetadata(""));
        public static readonly DependencyProperty LastRevenueProperty =
           DependencyProperty.Register("LastRevenue", typeof(string), typeof(RevenueBadgeUC), new PropertyMetadata(""));
        public string RevenueType
        {
            get { return (string)GetValue(RevenueTypeProperty); }
            set { SetValue(RevenueTypeProperty, value); }
        }

        public string CurrentRevenue
        {
            get { return (string)GetValue(CurrentRevenueProperty); }
            set { SetValue(CurrentRevenueProperty, value); }
        }
        public string LastRevenue
        {
            get { return (string)GetValue(LastRevenueProperty); }
            set { SetValue(LastRevenueProperty, value); }
        }

        public string BadgeText
        {
            get { return (string)GetValue(BadgeTextProperty); }
            set { SetValue(BadgeTextProperty, value); }
        }

        public Brush BadgeColor
        {
            get { return (Brush)GetValue(BadgeForegroundProperty); }
            set { SetValue(BadgeForegroundProperty, value); }
        }


        public RevenueBadgeUC()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);

            while (parent != null && !(parent is DashboardPage))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent != null)
            {
                //DashboardPage dashboardPage = parent as DashboardPage;
                //dashboardPage?.AnalythicsPageButton_Click(sender, e);
            
            }

        }

    }
}
