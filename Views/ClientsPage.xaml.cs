using ShopApp.DataAcces;

namespace ShopApp.Views;

public partial class ClientsPage : ContentPage
{
    private readonly ShopDbContext _db;

    public ClientsPage(ShopDbContext db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClientsCollection.ItemsSource = _db.Clients.ToList();
    }
}
