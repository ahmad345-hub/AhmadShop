
namespace AhmadShop.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }

        public decimal Discount { get; set; }

        public string MainImag { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public List<ProductTranslationResponse> Translations { get; set; } = new();
    }
}
