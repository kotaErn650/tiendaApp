using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess;
using ShopApp.Pages;

namespace ShopApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Registrar DbContext con InMemory
        builder.Services.AddDbContext<ShopDbContext>(options =>
            options.UseInMemoryDatabase("ShopComputer"));

        // Registrar vistas (Views)
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductDetailPage>();
        builder.Services.AddTransient<ClientsPage>();

        // Registrar rutas de navegación Maestro-Detalle
        Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));

        return builder.Build();
    }
}
