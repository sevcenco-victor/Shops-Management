using Shop.ApplicationServices.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Shop.Presentation.UserControls
{
    public partial class CountryPercentageUC : UserControl
    {

        public static readonly DependencyProperty CountryIsoCodeProperty
                    = DependencyProperty.Register(
                  "CountryIso", typeof(string), typeof(CountryPercentageUC), new PropertyMetadata("", OnCountryIsoCodeChanged));
        public string CountryIso
        {
            get { return (string)GetValue(CountryIsoCodeProperty); }
            set { SetValue(CountryIsoCodeProperty, value); }
        }
        private static void OnCountryIsoCodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CountryPercentageUC;
            if (control != null)
            {
                string isoCode = e.NewValue as string;
                if (!string.IsNullOrEmpty(isoCode))
                {
                    BitmapImage flagImage = FlagHelper.GetFlagImage(isoCode);
                    control.FlagImage.Source = flagImage;
                }
            }
        }
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register("ProgressValue", typeof(double), typeof(CountryPercentageUC), new PropertyMetadata(0.0));

        public double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }
        public CountryPercentageUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }

    }
}
