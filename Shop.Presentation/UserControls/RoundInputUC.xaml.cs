using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Shop.Presentation.UserControls
{
    public partial class RoundInputUC : UserControl
    {
        public static DependencyProperty InputCornerRadiusProperty
            = DependencyProperty.Register("InputConrnerRadius", typeof(string), typeof(RoundInputUC), new PropertyMetadata(""));

        public string InputCornerRadius
        {
            get { return (string)GetValue(InputCornerRadiusProperty); }
            set { SetValue(InputCornerRadiusProperty, value); }
        }
        public static DependencyProperty InputBorderBrushProperty
            = DependencyProperty.Register("InputBorderBrush", typeof(Brush), typeof(RoundInputUC), new PropertyMetadata(Brushes.Black));

        public Brushes InputBorderBrush
        {
            get { return (Brushes)GetValue(InputBorderBrushProperty); }
            set { SetValue(InputBorderBrushProperty, value); }
        }

        public static DependencyProperty InputPaddingProperty
            = DependencyProperty.Register("InputPadding", typeof(string), typeof(RoundInputUC), new PropertyMetadata(""));

        public string InputPadding
        {
            get { return (string)GetValue(InputPaddingProperty); }
            set { SetValue(InputPaddingProperty, value); }
        }
        public static DependencyProperty InputBorderThicknessProperty
            = DependencyProperty.Register("InputBorderThickness", typeof(string), typeof(RoundInputUC), new PropertyMetadata(""));

        public string InputBorderThickness
        {
            get { return (string)GetValue(InputBorderThicknessProperty); }
            set { SetValue(InputBorderThicknessProperty, value); }
        }

        public RoundInputUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
