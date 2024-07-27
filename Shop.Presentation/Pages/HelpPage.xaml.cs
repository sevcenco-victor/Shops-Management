using System.Windows;
using System.Windows.Controls;

namespace Shop.Presentation.Pages
{
    public class Step
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public partial class HelpPage : Page
    {
        public HelpPage()
        {
            InitializeComponent();

            MainWindow.SetPageHeaderName("Help");
            videoTutorial.Source = new Uri(System.IO.Directory.GetCurrentDirectory().ToString() + @"/Resources/AppTutorial.mp4");
            videoTutorial.Pause();
            InitSteps();
        }
        private void InitSteps()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new HelpWindow().Show();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            videoTutorial.Play();
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            videoTutorial.Pause();
        }
        private void Repeat_Click(object sender, RoutedEventArgs e)
        {
            videoTutorial.Position = TimeSpan.Zero;
            videoTutorial.Play();
        }
    }
}
