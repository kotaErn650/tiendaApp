using ShopApp.DataAccess;

namespace ShopApp;

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
        // Asegurar seed data cargada
        _db.Database.EnsureCreated();

        LabelCategorias.Text = _db.Categories.Count().ToString();
        LabelProductos.Text  = _db.Products.Count().ToString();
        LabelClientes.Text   = _db.Clients.Count().ToString();
    }
}
