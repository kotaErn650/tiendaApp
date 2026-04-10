namespace ShopApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
