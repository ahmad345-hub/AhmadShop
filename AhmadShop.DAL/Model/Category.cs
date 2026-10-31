using AhmadShop.DAL.Model;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool Status { get; set; }

    public List<CategoryTranslation> Translations { get; set; } = new();
}