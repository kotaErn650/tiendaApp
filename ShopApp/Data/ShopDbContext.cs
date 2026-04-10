using Microsoft.EntityFrameworkCore;
using ShopApp.Models;

namespace ShopApp.Data;

public class ShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("ShopComputer");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Nombre = "Laptops" },
            new Category { Id = 2, Nombre = "Smartphones" },
            new Category { Id = 3, Nombre = "Accesorios" }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Nombre = "Laptop Pro 15", Descripcion = "Laptop de alto rendimiento con pantalla 15\"", Precio = 1299.99m, CategoryId = 1 },
            new Product { Id = 2, Nombre = "Laptop Ultra Slim", Descripcion = "Laptop delgada y liviana para viajes", Precio = 899.99m, CategoryId = 1 },
            new Product { Id = 3, Nombre = "Smartphone X12", Descripcion = "Teléfono inteligente con cámara de 108MP", Precio = 749.99m, CategoryId = 2 },
            new Product { Id = 4, Nombre = "Smartphone Budget", Descripcion = "Teléfono económico con buena batería", Precio = 199.99m, CategoryId = 2 },
            new Product { Id = 5, Nombre = "Teclado Mecánico", Descripcion = "Teclado mecánico RGB para gaming", Precio = 89.99m, CategoryId = 3 },
            new Product { Id = 6, Nombre = "Mouse Inalámbrico", Descripcion = "Mouse ergonómico de alta precisión", Precio = 49.99m, CategoryId = 3 },
            new Product { Id = 7, Nombre = "Monitor 4K", Descripcion = "Monitor UHD de 27 pulgadas", Precio = 549.99m, CategoryId = 3 }
        );

        // Seed Clients
        modelBuilder.Entity<Client>().HasData(
            new Client { Id = 1, Nombre = "Ana García", Direccion = "Calle Principal 123, Madrid" },
            new Client { Id = 2, Nombre = "Carlos López", Direccion = "Av. Libertad 456, Barcelona" },
            new Client { Id = 3, Nombre = "María Fernández", Direccion = "Plaza Mayor 789, Valencia" },
            new Client { Id = 4, Nombre = "Juan Martínez", Direccion = "C/ Gran Vía 321, Sevilla" }
        );
    }
}
