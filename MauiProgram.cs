using Microsoft.EntityFrameworkCore;
using ShopApp.DataAcces;
using ShopApp.Views;

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

        // Registro del DbContext InMemory
        builder.Services.AddDbContext<ShopDbContext>(options =>
            options.UseInMemoryDatabase("ShopComputer"));

        // Registro de Vistas
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductDetailPage>();
        builder.Services.AddTransient<ClientsPage>();

        // Registro de rutas de navegación Maestro-Detalle
        Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));

        return builder.Build();
    }
}
