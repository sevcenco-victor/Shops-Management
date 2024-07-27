using LiveCharts;
using LiveCharts.Wpf;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using Shop.Presentation.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Shop.Applications;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Shop.ApplicationServices.Services;

namespace Shop.Presentation.Pages
{
    public partial class DashboardPage : Page
    {
        public SeriesCollection Series { get; set; } = new SeriesCollection();
        private readonly NexusDbContext _context = new NexusDbContext();
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;
        private List<TransactionRowUC> TransactionsList = [];

        public event PropertyChangedEventHandler PropertyChanged;

        private ChartValues<double> _chartValues = [];
        private List<string> _axisXLabels = [];
        private double _minValue = 0.0;
        private double _maxValue = 0.0;


        public ChartValues<double> ChartValues
        {
            get => _chartValues;
            set
            {
                _chartValues = value;
                OnPropertyChanged();
            }
        }
        public List<string> AxisXLabels
        {
            get => _axisXLabels;
            set
            {
                _axisXLabels = value;
                OnPropertyChanged();
            }
        }
        public double MinValue
        {
            get => _minValue;
            set
            {
                _minValue = value;
                OnPropertyChanged();
            }
        }
        public double MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                OnPropertyChanged();
            }
        }
        public DashboardPage()
        {
            _orderRepository = new OrderRepository(_context);
            _productRepository = new ProductRepository(_context);

            InitializeComponent();
            MainWindow.SetPageHeaderName("Dashboard");

            InitMonthBadge();
            InitDailyBadge();
            InitOrdersBadge();

            ChangeWeekGraph();
            lastGraphClickedButton = WeekButton;

            WeekButton.BorderThickness = new Thickness(0, 0, 0, 2);
            WeekButton.BorderBrush = (SolidColorBrush)Application.Current.FindResource("aqua");
            WeekButton.buttonText.Foreground = (SolidColorBrush)Application.Current.FindResource("aqua");

            InitTransctionBadge();

            DataContext = this;
        }
        private GraphChangeButtonUC lastGraphClickedButton;

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            GraphChangeButtonUC clickedButton = (GraphChangeButtonUC)sender;

            if (lastGraphClickedButton != null)
            {
                lastGraphClickedButton.BorderThickness = new Thickness(0);
                lastGraphClickedButton.buttonText.Foreground = (SolidColorBrush)Application.Current.FindResource("gray");
            }
            clickedButton.BorderThickness = new Thickness(0, 0, 0, 2);
            clickedButton.BorderBrush = (SolidColorBrush)Application.Current.FindResource("aqua");
            clickedButton.buttonText.Foreground = (SolidColorBrush)Application.Current.FindResource("aqua");

            lastGraphClickedButton = clickedButton;

            switch (clickedButton.ButtonText.ToLower())
            {
                case "day":
                    ChangeDayGraph();
                    break;
                case "week":
                    ChangeWeekGraph();
                    break;
                case "month":
                    ChangeMonthGraph();
                    break;
            }

            DataContext = null;
            DataContext = this;

        }
        private void ChangeDayGraph()
        {
            AxisXLabels = new List<string>();
            ChartValues = new ChartValues<double>();
            Dictionary<string, double> today = CartesianChartServices.GetThreeHourlyRevenue(DateTime.Now);

            MinValue = today.Values.Min();
            MaxValue = today.Values.Max();

            foreach (var value in today)
            {
                AxisXLabels.Add(value.Key);
                ChartValues.Add(value.Value);
            }
        }

        private void ChangeWeekGraph()
        {
            AxisXLabels = new List<string>();
            ChartValues = new ChartValues<double>();
            Dictionary<string, double> today = CartesianChartServices.GetRevenueForWeekDays(DateTime.Now);

            MinValue = today.Values.Min();
            MaxValue = today.Values.Max();

            foreach (var value in today)
            {
                AxisXLabels.Add(value.Key);
                ChartValues.Add(value.Value);
            }
        }

        private void ChangeMonthGraph()
        {
            AxisXLabels = new List<string>();
            ChartValues = new ChartValues<double>();
            Dictionary<string, double> today = CartesianChartServices.GetRevenueForFiveDayIntervals(DateTime.Now);

            MinValue = today.Values.Min();
            MaxValue = today.Values.Max();

            foreach (var value in today)
            {
                AxisXLabels.Add(value.Key);
                ChartValues.Add(value.Value);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void InitTransctionBadge()
        {
            foreach (var trans in TransactionRowUC.GetRows(5))
            {
                transactionWrapper.Children.Add(trans);
            }
        }
        private void InitMonthBadge()
        {
            double? currMonthRev = GetMonthRevenue(DateTime.Now);
            double? lastMonthRev = GetMonthRevenue(DateTime.Now.AddMonths(-1));

            double? percentageDifference = (currMonthRev - lastMonthRev) / lastMonthRev * 100;
            string badgePercentage = (percentageDifference > 0 ? "+" : "") + percentageDifference?.ToString("N2");


            RevenueBadgeUC monthlyRevenue = new RevenueBadgeUC()
            {
                RevenueType = "MONTHLY REVENUE",
                CurrentRevenue = currMonthRev?.ToString("C2"),
                BadgeText = badgePercentage + "%",
                BadgeColor = (Brush)Application.Current.FindResource(percentageDifference > 0 ? "green" : "red"),
                LastRevenue = "Last Month: " + lastMonthRev?.ToString("C2"),
            };
            Grid.SetColumn(monthlyRevenue, 0);
            monthlyRevenue.Margin = new Thickness(0, 0, 12, 0);

            revenueCards.Children.Add(monthlyRevenue);
        }
        private void InitDailyBadge()
        {
            double? currDayRev = GetDailyRevenue(DateTime.Today);
            double? prevDayRev = GetDailyRevenue(DateTime.Today.AddDays(-1));

            double? percentageDifference = prevDayRev != 0 ? (currDayRev - prevDayRev) / prevDayRev * 100 : 0;
            string badgePercentage = (percentageDifference > 0 ? "+" : "") + percentageDifference?.ToString("N2");

            RevenueBadgeUC dailyRevenue = new RevenueBadgeUC()
            {
                RevenueType = "TODAY'S REVENUE",
                CurrentRevenue = currDayRev?.ToString("C2"),
                BadgeText = badgePercentage + "%",
                BadgeColor = (Brush)Application.Current.FindResource(percentageDifference > 0 ? "green" : "red"),
                LastRevenue = "Yesterday: " + prevDayRev?.ToString("C2"),
            };
            Grid.SetColumn(dailyRevenue, 1);
            dailyRevenue.Margin = new Thickness(12, 0, 12, 0);

            revenueCards.Children.Add(dailyRevenue);
        }
        private void InitOrdersBadge()
        {
            int currDayOrders = GetDailyOrdersCount(DateTime.Today);
            int prevDayOrders = GetDailyOrdersCount(DateTime.Today.AddDays(-1));

            double percentageDifference = 0;
            if (prevDayOrders != 0)
            {
                percentageDifference = ((double)(currDayOrders - prevDayOrders) / prevDayOrders) * 100;
            }
            string badgePercentage = (percentageDifference > 0 ? "+" : "") + percentageDifference.ToString("N2");

            RevenueBadgeUC dailyOrders = new RevenueBadgeUC()
            {
                RevenueType = "TODAY'S ORDERS",
                CurrentRevenue = currDayOrders + " units",
                BadgeText = badgePercentage + "%",
                BadgeColor = (Brush)Application.Current.FindResource(percentageDifference > 0 ? "green" : "red"),
                LastRevenue = "Yesterday: " + prevDayOrders + " units",
            };
            Grid.SetColumn(dailyOrders, 2);
            dailyOrders.Margin = new Thickness(12, 0, 0, 0);
            revenueCards.Children.Add(dailyOrders);
        }

        private double? GetMonthRevenue(DateTime month)
        {
            DateTime startOfMonth = new DateTime(month.Year, month.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            IEnumerable<OrderEntity> ordersInMonth = _orderRepository
                .GetAllOrders()
                .Where(o => o.OrderDate >= startOfMonth && o.OrderDate <= endOfMonth);

            double totalRevenue = ordersInMonth.Sum(o => o.Quantity * _productRepository.GetProductById(o.ProductId).Price);

            return totalRevenue;
        }
        private double? GetDailyRevenue(DateTime date)
        {
            DateTime startOfDay = date.Date;
            DateTime endOfDay = date.Date.AddDays(1).AddTicks(-1);

            IEnumerable<OrderEntity> ordersOnDay = _orderRepository
                .GetAllOrders()
                .Where(o => o.OrderDate >= startOfDay && o.OrderDate <= endOfDay);

            double totalRevenue = ordersOnDay.Sum(o => o.Quantity * _productRepository.GetProductById(o.ProductId).Price);

            return totalRevenue;
        }

        private int GetDailyOrdersCount(DateTime date)
        {
            DateTime startOfDay = date.Date;
            DateTime endOfDay = date.Date.AddDays(1).AddTicks(-1);

            IEnumerable<OrderEntity> ordersOnDay = _orderRepository
                .GetAllOrders()
                .Where(o => o.OrderDate >= startOfDay && o.OrderDate <= endOfDay);

            int ordersCount = ordersOnDay.Count();

            return ordersCount;
        }

    }
}
