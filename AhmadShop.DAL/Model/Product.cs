namespace AhmadShop.DAL.Model
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public double Rate { get; set; }
        public decimal Discount { get; set; }
        public string MainImag { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<ProductTranslation> Translations { get; set; } = new();
    }
}