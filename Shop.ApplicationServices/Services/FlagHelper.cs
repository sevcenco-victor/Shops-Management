using System.Windows.Media.Imaging;

namespace Shop.ApplicationServices.Services
{
    public static class FlagHelper
    {
        public static BitmapImage? GetFlagImage(string isoCode)
        {
            try
            {
                // The flag images are named after the ISO 3166-alpha1 2 letter country code
                var flagIcon = $"/FamFamFam.Flags.Wpf;component/Images/{isoCode}.png";
                var flagImage = new BitmapImage(new Uri(flagIcon, UriKind.Relative));
                return flagImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading flag for {isoCode}: {ex.Message}");
                return null;
            }
        }
    }
}
