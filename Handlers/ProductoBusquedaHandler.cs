using System.Collections.ObjectModel;
using ShopApp.DataAcces;
using ShopApp.Models;
using ShopApp.Views;

namespace ShopApp.Handlers;

public class ProductoBusquedaHandler : SearchHandler
{
    private ShopDbContext? _db;
    private List<Product> _allProducts = new();

    public ProductoBusquedaHandler() { }

    public void Initialize(ShopDbContext db)
    {
        _db = db;
        _allProducts.Clear();
    }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (_db is null) return;

        if (_allProducts.Count == 0)
            _allProducts = _db.Products.ToList();

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = _allProducts
                .Where(p => p.Nombre.Contains(newValue, StringComparison.OrdinalIgnoreCase)
                         || p.Descripcion.Contains(newValue, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        if (item is Product product)
        {
            Query = product.Nombre;
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={product.Id}");
        }
    }
}
