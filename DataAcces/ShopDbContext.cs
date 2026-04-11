using Microsoft.EntityFrameworkCore;
using ShopApp.Models;

namespace ShopApp.DataAcces;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseInMemoryDatabase("ShopComputer");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la clave primaria para records
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Product>().HasKey(p => p.Id);

        // Seed: 7 Categorías
        modelBuilder.Entity<Category>().HasData(
            new Category(1, "Laptops"),
            new Category(2, "Monitores"),
            new Category(3, "Teclados"),
            new Category(4, "Ratones"),
            new Category(5, "Auriculares"),
            new Category(6, "Impresoras"),
            new Category(7, "Webcams")
        );

        // Seed: 2 Clientes
        modelBuilder.Entity<Client>().HasData(
            new Client(1, "Juan Pérez", "Av. Principal 123, Ciudad"),
            new Client(2, "María García", "Calle Secundaria 456, Municipio")
        );

        // Seed: 7 Productos con precios variados
        modelBuilder.Entity<Product>().HasData(
            new Product(1, "Laptop Pro 15",    "Laptop de alto rendimiento 15\"",   1299.99m, 1),
            new Product(2, "Monitor UltraWide","Monitor curvo 34\" 144Hz",           549.99m,  2),
            new Product(3, "Teclado Mecánico", "Teclado mecánico RGB switches Blue",  89.99m,  3),
            new Product(4, "Ratón Gamer",      "Ratón óptico 16000 DPI",              59.99m,  4),
            new Product(5, "Auriculares BT",   "Auriculares inalámbricos cancelación de ruido", 199.99m, 5),
            new Product(6, "Impresora Láser",  "Impresora láser monocromática",      249.99m,  6),
            new Product(7, "Webcam HD",        "Cámara web 1080p con micrófono",      79.99m,  7)
        );
    }
}
