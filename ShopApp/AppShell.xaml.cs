using ShopApp.Views;

namespace ShopApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register named routes for detail pages (not in tab bar)
        Routing.RegisterRoute("productdetail", typeof(ProductDetailPage));
    }
}
