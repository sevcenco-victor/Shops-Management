using Bogus;
using Shop.Applications;
using Shop.Domain.Models;
using System.Globalization;
using System.Windows.Forms;

namespace Shop.ApplicationServices.Services
{
    public static class ClientService
    {
        public static Faker<ClientEntity> GenerateRandomClient()
        {
            return new Faker<ClientEntity>()
                .RuleFor(c => c.Image, f => ImageToByteConverter.ConvertImageToByteArray(SelectImage()))
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Name.ToLower().Replace(" ", ".")))
                .RuleFor(c => c.Gender, f => f.PickRandom("Male", "Female"))
                .RuleFor(c => c.Address, f => f.Address.StreetAddress())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.Country, f => GetCountryISOCode(f.Address.Country()));
        }
        private static string GetCountryISOCode(string countryName)
        {
            var cultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (var cultureInfo in cultureInfos)
            {
                var region = new RegionInfo(cultureInfo.Name);
                if (region.EnglishName.Contains(countryName))
                {
                    return region.TwoLetterISORegionName;
                }
            }
            return "UK";
        }
        private static string SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            string selectedImagePath = string.Empty;
            if (openFileDialog.ShowDialog() is System.Windows.Forms.DialogResult.OK)
                selectedImagePath = openFileDialog.FileName;

            return selectedImagePath;
        }

    }
}
