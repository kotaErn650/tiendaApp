namespace ShopApp.Models;

public record Product(int Id, string Nombre, string Descripcion, decimal Precio, int CategoryId);
