using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Shop.Applications
{
    public static class ImageToByteConverter
    {
        public static byte[]? ConvertImageToByteArray(string imagePath)
        {
            try
            {
                byte[] imageData = File.ReadAllBytes(imagePath);
                return imageData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting image to byte array: " + ex.Message);
                return null;
            }
        }
        public static ImageSource ConvertByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
        }
    }
}
