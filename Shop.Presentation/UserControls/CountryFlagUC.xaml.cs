using System.Windows;
using System.Windows.Controls;

namespace Shop.Presentation.UserControls
{
    public partial class CountryFlagUC : UserControl
    {
        private static DependencyProperty CountryNamePropery =
            DependencyProperty.Register("CountryName", typeof(string), typeof(CountryFlagUC), new PropertyMetadata(""));
        public string CountryName
        {
            get { return (string)GetValue(CountryNamePropery); }
            set { SetValue(CountryNamePropery, value); }
        }
        public CountryFlagUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }

}