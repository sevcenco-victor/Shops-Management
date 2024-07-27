using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shop.Presentation.UserControls
{
    public partial class RoundButtonUC : UserControl
    {
        public static readonly DependencyProperty newBackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(RoundButtonUC), new PropertyMetadata(default(Brush)));

        public Brush Background
        {
            get { return (Brush)GetValue(newBackgroundProperty); }
            set { SetValue(newBackgroundProperty, value); }
        }

        public static readonly DependencyProperty newBorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(RoundButtonUC), new PropertyMetadata(default(Brush)));

        public Brush BorderBrush
        {
            get { return (Brush)GetValue(newBorderBrushProperty); }
            set { SetValue(newBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty newForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(RoundButtonUC), new PropertyMetadata(default(Brush)));

        public Brush Foreground
        {
            get { return (Brush)GetValue(newForegroundProperty); }
            set { SetValue(newForegroundProperty, value); }
        }

        public static readonly DependencyProperty newContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(RoundButtonUC), new PropertyMetadata(null));

        public object Content
        {
            get { return (object)GetValue(newContentProperty); }
            set { SetValue(newContentProperty, value); }
        }

        public static readonly DependencyProperty newPaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(RoundButtonUC), new PropertyMetadata(new Thickness()));

        public Thickness Padding
        {
            get { return (Thickness)GetValue(newPaddingProperty); }
            set { SetValue(newPaddingProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(string), typeof(RoundButtonUC), new PropertyMetadata(""));

        public string ButtonCornerRadius
        {
            get { return (string)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonClickEventProperty =
           DependencyProperty.Register("ButtonClickEvent", typeof(string), typeof(RoundButtonUC), new PropertyMetadata(""));

        public string ButtonClickEvent
        {
            get { return (string)GetValue(ButtonClickEventProperty); }
            set { SetValue(ButtonClickEventProperty, value); }
        }
        public RoundButtonUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}