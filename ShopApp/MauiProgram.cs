using Microsoft.Extensions.Logging;
using ShopApp.DataAcces;
using ShopApp.Views;

namespace ShopApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMaui()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register EF Core InMemory DbContext
        builder.Services.AddDbContext<ShopDbContext>();

        // Register pages for DI / navigation
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<ProductDetailPage>();
        builder.Services.AddTransient<ClientsPage>();

        // Register named route for the detail page
        Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Ensure the in-memory database is seeded before returning
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
            db.Database.EnsureCreated();
        }

        return app;
    }
}
