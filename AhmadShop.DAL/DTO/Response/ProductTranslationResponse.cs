
namespace AhmadShop.DAL.DTO.Response
{
    public class ProductTranslationResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Language { get; set; } = string.Empty;
    }
}
