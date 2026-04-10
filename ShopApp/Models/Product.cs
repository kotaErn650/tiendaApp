namespace ShopApp.Models;

public record Product
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
