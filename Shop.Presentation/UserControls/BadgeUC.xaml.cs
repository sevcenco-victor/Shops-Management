using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shop.Presentation.UserControls
{
    public partial class BadgeUC : UserControl
    {
        public static readonly DependencyProperty BadgeTextProperty =
            DependencyProperty.Register("BadgeText", typeof(string), typeof(BadgeUC), new PropertyMetadata(""));

        public string BadgeText
        {
            get { return (string)GetValue(BadgeTextProperty); }
            set { SetValue(BadgeTextProperty, value); }
        }

        public static readonly DependencyProperty BadgeColorProperty =
            DependencyProperty.Register("BadgeColor", typeof(Brush), typeof(BadgeUC), new PropertyMetadata(Brushes.Green));

        public Brush BadgeColor
        {
            get { return (Brush)GetValue(BadgeColorProperty); }
            set { SetValue(BadgeColorProperty, value); }
        }

        public BadgeUC()
        {
            InitializeComponent();
        }
    }
}
