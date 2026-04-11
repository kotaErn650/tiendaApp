using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess;

namespace ShopApp.Pages;

public partial class ProductsPage : ContentPage
{
    private readonly ShopDbContext _db;
    public ObservableCollection<Product> Products { get; } = new();

    public ProductsPage(ShopDbContext db)
    {
        InitializeComponent();
        _db = db;
        ProductsCollection.ItemsSource = Products;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _db.Database.EnsureCreated();
        Products.Clear();
        foreach (var p in _db.Products.AsNoTracking().ToList())
            Products.Add(p);
    }

    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Product selected)
        {
            // Deselect
            ((CollectionView)sender).SelectedItem = null;
            await Shell.Current.GoToAsync(
                $"{nameof(ProductDetailPage)}?id={selected.Id}");
        }
    }
}
