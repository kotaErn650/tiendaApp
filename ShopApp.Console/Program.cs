using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopApp.Core.DataAccess;

// ─── Configuración de DI y DbContext ────────────────────────────────────────
var services = new ServiceCollection();
services.AddDbContext<ShopDbContext>(options =>
    options.UseInMemoryDatabase("ShopComputer"));
var provider = services.BuildServiceProvider();

using var db = provider.GetRequiredService<ShopDbContext>();
db.Database.EnsureCreated();

// ─── Encabezado ──────────────────────────────────────────────────────────────
Console.OutputEncoding = System.Text.Encoding.UTF8;
PrintHeader("🛒  ShopApp — Demo de Consola");

// ─── Resumen ──────────────────────────────────────────────────────────────────
int totalCategorias = await db.Categories.CountAsync();
int totalProductos   = await db.Products.CountAsync();
int totalClientes    = await db.Clients.CountAsync();

PrintSection("📊 Resumen");
Console.WriteLine($"  Categorías : {totalCategorias}");
Console.WriteLine($"  Productos  : {totalProductos}");
Console.WriteLine($"  Clientes   : {totalClientes}");

// ─── Categorías ───────────────────────────────────────────────────────────────
PrintSection("🗂️  Categorías");
var categorias = await db.Categories.OrderBy(c => c.Id).ToListAsync();
foreach (var cat in categorias)
    Console.WriteLine($"  [{cat.Id}] {cat.Nombre}");

// ─── Productos ────────────────────────────────────────────────────────────────
PrintSection("📦 Productos");
Console.WriteLine($"  {"ID",-4} {"Nombre",-22} {"Precio",10}  {"Cat",4}  Descripción");
Console.WriteLine($"  {new string('─', 75)}");
var productos = await db.Products.OrderBy(p => p.Id).ToListAsync();
foreach (var p in productos)
{
    string color = p.Precio <= 100m ? "✅" : "🟣";
    Console.WriteLine($"  {p.Id,-4} {p.Nombre,-22} {p.Precio,9:C}  {p.CategoryId,3}  {color} {p.Descripcion}");
}

// ─── Clientes ────────────────────────────────────────────────────────────────
PrintSection("👥 Clientes");
var clientes = await db.Clients.OrderBy(c => c.Id).ToListAsync();
foreach (var c in clientes)
    Console.WriteLine($"  [{c.Id}] {c.Nombre}  —  {c.Direccion}");

// ─── Demo: detalle de producto ────────────────────────────────────────────────
PrintSection("🔍 Detalle de Producto (ID = 1)");
var producto = await db.Products.FindAsync(1);
if (producto is not null)
{
    var categoria = await db.Categories.FindAsync(producto.CategoryId);
    Console.WriteLine($"  Nombre      : {producto.Nombre}");
    Console.WriteLine($"  Descripción : {producto.Descripcion}");
    Console.WriteLine($"  Precio      : {producto.Precio:C}");
    Console.WriteLine($"  Categoría   : {categoria?.Nombre ?? "—"}");

    // Simular selector de cantidad (como el Stepper de la UI MAUI)
    Console.WriteLine();
    Console.WriteLine("  Simulación de Pedido:");
    for (int qty = 1; qty <= 3; qty++)
        Console.WriteLine($"    Cantidad: {qty}  →  Total: {producto.Precio * qty:C}");
}

// ─── Demo: filtro de productos ────────────────────────────────────────────────
PrintSection("🔎 Búsqueda: productos con precio > $100");
var caros = await db.Products.Where(p => p.Precio > 100m).OrderByDescending(p => p.Precio).ToListAsync();
foreach (var p in caros)
    Console.WriteLine($"  {p.Nombre,-22} {p.Precio,10:C}");

Console.WriteLine();
PrintHeader("✔  Ejecución completada sin errores");

// ─── Helpers ─────────────────────────────────────────────────────────────────
static void PrintHeader(string title)
{
    var line = new string('═', title.Length + 4);
    Console.WriteLine();
    Console.WriteLine($"  ╔{line}╗");
    Console.WriteLine($"  ║  {title}  ║");
    Console.WriteLine($"  ╚{line}╝");
    Console.WriteLine();
}

static void PrintSection(string title)
{
    Console.WriteLine();
    Console.WriteLine($"  ── {title} ──────────────────────────────────────────");
}
