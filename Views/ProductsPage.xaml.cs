using System.Collections.ObjectModel;
using ShopApp.DataAcces;
using ShopApp.Handlers;
using ShopApp.Models;

namespace ShopApp.Views;

public partial class ProductsPage : ContentPage
{
    private readonly ShopDbContext _db;
    public ObservableCollection<Product> Products { get; } = new();

    public ProductsPage(ShopDbContext db)
    {
        InitializeComponent();
        _db = db;
        BindingContext = this;
        BusquedaHandler.Initialize(db);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Products.Clear();
        foreach (var p in _db.Products.ToList())
            Products.Add(p);
    }

    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Product product)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={product.Id}");
        }
    }
}
