using System.Windows;
using System.Windows.Controls;

namespace Shop.Presentation.UserControls
{
    public partial class GraphChangeButtonUC : UserControl
    {
        public static readonly DependencyProperty ButtonTextProperty =
          DependencyProperty.Register("ButtonText", typeof(string), typeof(GraphChangeButtonUC), new PropertyMetadata(""));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }


        public GraphChangeButtonUC()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler Click;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
