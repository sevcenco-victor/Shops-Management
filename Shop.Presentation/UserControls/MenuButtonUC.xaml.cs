using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Shop.Presentation.UserControls
{
    public partial class MenuButtonUC : UserControl
    {

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(MenuButtonUC), new PropertyMetadata("Textul initial", OnTextChanged));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register("ImagePath", typeof(string), typeof(MenuButtonUC), new PropertyMetadata("", OnImagePathChanged));

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }


        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MenuButtonUC)d;
            control.buttonText.Text = e.NewValue.ToString();
        }

        public event RoutedEventHandler Click;
        public MenuButtonUC()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        private static void OnImagePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MenuButtonUC)d;
            BitmapImage newImage = new BitmapImage();
            newImage.BeginInit();
            newImage.UriSource = new Uri(e.NewValue.ToString(), UriKind.Relative);
            newImage.EndInit();

            control.buttonImage.Source = newImage;
        }

        private void customButton_Click(object sender, EventArgs e)
        {

        }
    }
}
