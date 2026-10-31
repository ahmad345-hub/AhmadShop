
namespace AhmadShop.DAL.DTO.Requests
{
    public class CategoryRequest
    {
        public string Name { get; set; } = string.Empty;

        public bool Status { get; set; }

        public List<CategoryTranslationRequest> Translations { get; set; } = new();
    }
}
