using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using System.Diagnostics.Metrics;
using System.Windows.Controls;

namespace Shop.Presentation.UserControls
{
    public partial class CountryRateBadgeUC : UserControl
    {
        private static readonly NexusDbContext _context = new NexusDbContext();
        private static readonly List<OrderEntity> _orders = _context.Orders.ToList();
        private static readonly ClientRepository _clientRepository = new ClientRepository(_context);
        public CountryRateBadgeUC()
        {
            InitializeComponent();
            InitBadge();
        }
        public void InitBadge()
        {
            int totalOrders = _orders.Count;

            Dictionary<string, int> countryCount = GetCountryRate();

            var topCountries = countryCount.OrderByDescending(pair => pair.Value).Take(4).ToList();

            foreach (var country in topCountries)
            {
                double countryPercentage = country.Value * 100.0 / totalOrders;
                CountryPercentageUC countryRow = new CountryPercentageUC()
                {
                    CountryIso = country.Key,
                    ProgressValue = countryPercentage,
                };
                countryWrapper.Children.Add(countryRow);
            }

            double othersPercentage = (totalOrders - topCountries.Sum(pair => pair.Value)) * 100.0 / totalOrders;
            CountryPercentageUC countryRowOthers = new CountryPercentageUC()
            {
                CountryIso = "Others",
                ProgressValue = othersPercentage,
            };
            countryWrapper.Children.Add(countryRowOthers);
        }

        private Dictionary<string, int> GetCountryRate()
        {
            Dictionary<string, int> countryCount = new Dictionary<string, int>();

            foreach (var order in _orders)
            {
                string country = _clientRepository.GetClientById(order.ClientID).Country;

                if (!countryCount.ContainsKey(country))
                {
                    countryCount[country] = 1;
                }
                else
                {
                    countryCount[country]++;
                }
            }

            return countryCount;
        }

    }
}
