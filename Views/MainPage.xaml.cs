using ShopApp.DataAcces;

namespace ShopApp.Views;

public partial class MainPage : ContentPage
{
    private readonly ShopDbContext _db;

    public MainPage(ShopDbContext db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LblCategorias.Text = _db.Categories.Count().ToString();
        LblProductos.Text  = _db.Products.Count().ToString();
        LblClientes.Text   = _db.Clients.Count().ToString();
    }
}
