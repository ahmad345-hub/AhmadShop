
using AhmadShop.DAL.DTO.Response;
using AhmadShop.DAL.Model;
using Mapster;
using System.Globalization;

namespace AhmadShop.BLL.Settings
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(
                    dest => dest.Name,
                    src => src.Translations
                        .Where(t => t.Language == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                        .Select(t => t.Name)
                        .FirstOrDefault() ?? src.Name
                );
        }
    }
}
