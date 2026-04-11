using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess;

namespace ShopApp.Pages;

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
        _db.Database.EnsureCreated();
        ClientsCollection.ItemsSource = _db.Clients.AsNoTracking().ToList();
    }
}
