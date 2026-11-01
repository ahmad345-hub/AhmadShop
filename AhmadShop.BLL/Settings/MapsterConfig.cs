
using AhmadShop.DAL.DTO.Requests;
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

            TypeAdapterConfig<ProductRequest, Product>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.MainImag)
                .Map(dest => dest.Translations, src => src.Translations);

            TypeAdapterConfig<ProductTranslationRequest, ProductTranslation>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.ProductId)
                .Ignore(dest => dest.Product);

            TypeAdapterConfig<Product, ProductResponse>.NewConfig();

            TypeAdapterConfig<ProductTranslation, ProductTranslationResponse>.NewConfig();
        }
    }
}
