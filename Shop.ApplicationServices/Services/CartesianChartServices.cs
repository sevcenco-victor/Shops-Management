using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;

namespace Shop.ApplicationServices.Services
{
    public class CartesianChartServices
    {
        private static readonly NexusDbContext _context = new NexusDbContext();
        private static readonly OrderRepository _orderRepository = new OrderRepository(_context);
        private static readonly ProductRepository _productRepository = new ProductRepository(_context);

        public static Dictionary<string, double> GetThreeHourlyRevenue(DateTime date)
        {
            Dictionary<string, double> threeHourlyRevenueDict = new Dictionary<string, double>();

            for (int hour = 0; hour < 24; hour += 3)
            {
                string startHourString = $"{hour:D2}:00";
                string endHourString = $"{hour + 2:D2}:59";

                double threeHourlyRevenue = GetThreeHourlyRevenueForInterval(date, hour, hour + 2);
                string intervalString = $"{startHourString} - {endHourString}";
                threeHourlyRevenueDict.Add(intervalString, threeHourlyRevenue);
            }

            return threeHourlyRevenueDict;
        }

        private static double GetThreeHourlyRevenueForInterval(DateTime date, int startHour, int endHour)
        {
            DateTime startOfInterval = date.Date.AddHours(startHour);
            DateTime endOfInterval = date.Date.AddHours(endHour + 1).AddTicks(-1);

            IEnumerable<OrderEntity> ordersInInterval = _orderRepository
                .GetAllOrders()
                .Where(o => o.OrderDate >= startOfInterval && o.OrderDate <= endOfInterval);

            double intervalRevenue = ordersInInterval.Sum(o => o.Quantity * _productRepository.GetProductById(o.ProductId).Price);

            return intervalRevenue;
        }
        public static Dictionary<string, double> GetRevenueForFiveDayIntervals(DateTime date)
        {
            Dictionary<string, double> revenueForIntervals = new Dictionary<string, double>();

            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            for (int i = 0; i < 7; i++)
            {
                DateTime startOfInterval = firstDayOfMonth.AddDays(i * 5);

                DateTime endOfInterval = startOfInterval.AddDays(5).AddTicks(-1);

                double revenueForInterval = GetTotalRevenue(startOfInterval, endOfInterval);

                revenueForIntervals.Add(startOfInterval.ToString("dd"), revenueForInterval);
            }

            return revenueForIntervals;
        }
        public static Dictionary<string, double> GetRevenueForWeekDays(DateTime date)
        {
            Dictionary<string, double> revenueForWeekDays = new Dictionary<string, double>();

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = date.AddDays(-(int)date.DayOfWeek + i);

                double revenueForDay = GetTotalRevenue(currentDay.Date, currentDay.Date.AddDays(1).AddTicks(-1));

                revenueForWeekDays.Add(currentDay.ToString("dddd"), revenueForDay);
            }

            return revenueForWeekDays;
        }


        private static double GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            IEnumerable<OrderEntity> ordersInRange = _orderRepository
                .GetAllOrders()
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);

            double totalRevenue = ordersInRange.Sum(o => o.Quantity * _productRepository.GetProductById(o.ProductId).Price);

            return totalRevenue;
        }
    }
}
