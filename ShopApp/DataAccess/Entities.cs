namespace ShopApp.DataAccess;

public record Product(int Id, string Nombre, string Descripcion, decimal Precio, int CategoryId);

public record Client(int Id, string Nombre, string Direccion);

public record Category(int Id, string Nombre);
