using DocumentFormat.OpenXml.Presentation;
using Shop.Applications;
using Shop.ApplicationServices.Services;
using Shop.Domain.Models;
using Shop.Infrastructure;
using Shop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shop.Presentation.UserControls
{
    public partial class TransactionRowUC : UserControl
    {
        private static readonly NexusDbContext _context = new NexusDbContext();
        private static readonly ClientRepository _clientRepository = new ClientRepository(_context);
        private static readonly ProductRepository _productRepository = new ProductRepository(_context);

        public static readonly DependencyProperty UserNameProperty
            = DependencyProperty.Register("UserName", typeof(string), typeof(TransactionRowUC), new PropertyMetadata(""));
        public static readonly DependencyProperty UserEmailProperty
            = DependencyProperty.Register("UserEmail", typeof(string), typeof(TransactionRowUC), new PropertyMetadata(""));
        public static readonly DependencyProperty InvoiceNumberProperty
            = DependencyProperty.Register("InvoiceNumber", typeof(string), typeof(TransactionRowUC), new PropertyMetadata(""));
        public static readonly DependencyProperty DateProperty
            = DependencyProperty.Register("Date", typeof(string), typeof(TransactionRowUC), new PropertyMetadata(""));
        public static readonly DependencyProperty AmountProperty
            = DependencyProperty.Register("Amount", typeof(string), typeof(TransactionRowUC), new PropertyMetadata(""));
        public static readonly DependencyProperty BadgeTextProperty
            = DependencyProperty.Register("BadgeText", typeof(string), typeof(TransactionRowUC), new PropertyMetadata(""));
        public static readonly DependencyProperty BadgeForegroundProperty
            = DependencyProperty.Register("BadgeForeground", typeof(Brush), typeof(TransactionRowUC), new PropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty CountryIsoCodeProperty
            = DependencyProperty.Register(
          "CountryIsoCode", typeof(string), typeof(TransactionRowUC), new PropertyMetadata("", OnCountryIsoCodeChanged));

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public string UserEmail
        {
            get { return (string)GetValue(UserEmailProperty); }
            set { SetValue(UserEmailProperty, value); }
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
        public string InvoiceNumber
        {
            get { return (string)GetValue(InvoiceNumberProperty); }
            set { SetValue(InvoiceNumberProperty, value); }
        }
        public string CountryIsoCode
        {
            get { return (string)GetValue(CountryIsoCodeProperty); }
            set { SetValue(CountryIsoCodeProperty, value); }
        }
        private static void OnCountryIsoCodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TransactionRowUC;
            if (control != null)
            {
                string? isoCode = e.NewValue as string;
                if (!string.IsNullOrEmpty(isoCode))
                {
                    BitmapImage? flagImage = FlagHelper.GetFlagImage(isoCode);
                    control.FlagImage.Source = flagImage;
                }
            }
        }
        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public string Amount
        {
            get { return (string)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        public TransactionRowUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static IEnumerable<TransactionRowUC> GetRows(int numOfRows)
        {
            List<OrderEntity> orders = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(numOfRows)
                .ToList();
            foreach (OrderEntity order in orders)
            {
                ClientEntity? client = _clientRepository.GetClientById(order.ClientID);
                double orderTotal = _productRepository.GetProductById(order.ProductId).Price * order.Quantity;
                TransactionRowUC transRow = new TransactionRowUC()
                {
                    UserName = client.Name,
                    UserEmail = client.Email,
                    BadgeText = "Completed",
                    BadgeColor = Brushes.Green,
                    InvoiceNumber = order.Id.ToString(),
                    CountryIsoCode = client.Country,
                    Date = order.OrderDate.ToShortDateString(),
                    Amount = Math.Round(orderTotal,2).ToString("N2"),
                };
                transRow.userIcon.Source = ImageToByteConverter.ConvertByteArrayToImage(client.Image);

                yield return transRow;
            }
        }
    }
}
