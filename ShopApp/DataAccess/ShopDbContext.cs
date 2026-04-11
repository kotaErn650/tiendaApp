using Microsoft.EntityFrameworkCore;

namespace ShopApp.DataAccess;

public class ShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("ShopComputer");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar entidades record para EF Core
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Product>().HasKey(p => p.Id);

        // Seed: 7 Categorías
        modelBuilder.Entity<Category>().HasData(
            new Category(1, "Computadoras"),
            new Category(2, "Monitores"),
            new Category(3, "Teclados"),
            new Category(4, "Ratones"),
            new Category(5, "Auriculares"),
            new Category(6, "Impresoras"),
            new Category(7, "Almacenamiento")
        );

        // Seed: 7 Productos con precios variados
        modelBuilder.Entity<Product>().HasData(
            new Product(1, "Laptop Pro 15",    "Laptop de alto rendimiento, 16GB RAM, 512GB SSD",  1299.99m, 1),
            new Product(2, "Monitor 4K 27\"",  "Monitor Ultra HD con panel IPS y 144Hz",            499.00m, 2),
            new Product(3, "Teclado Mecánico", "Teclado mecánico retroiluminado RGB, switches Blue",  89.99m, 3),
            new Product(4, "Ratón Inalámbrico","Ratón ergonómico con DPI ajustable hasta 16000",     49.99m, 4),
            new Product(5, "Auriculares BT",   "Auriculares bluetooth con cancelación de ruido",    149.00m, 5),
            new Product(6, "Impresora Láser",  "Impresora láser monocromática 40ppm",               199.99m, 6),
            new Product(7, "SSD 1TB",          "Unidad de estado sólido NVMe PCIe 4.0",              99.50m, 7)
        );

        // Seed: 2 Clientes
        modelBuilder.Entity<Client>().HasData(
            new Client(1, "Ana García",    "Calle Mayor 10, Madrid"),
            new Client(2, "Carlos López", "Av. Libertad 55, Barcelona")
        );
    }
}
