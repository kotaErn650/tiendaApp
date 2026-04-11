using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess;

namespace ShopApp.Pages;

public class ProductoBusquedaHandler : SearchHandler
{
    private readonly ShopDbContext _db;

    public ProductoBusquedaHandler(ShopDbContext db)
    {
        _db = db;
        Placeholder = "Buscar producto...";
        ShowsResults = true;
    }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            var query = newValue.ToLowerInvariant();
            ItemsSource = _db.Products
                .AsNoTracking()
                .Where(p => p.Nombre.ToLower().Contains(query) ||
                            p.Descripcion.ToLower().Contains(query))
                .ToList();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        if (item is Product product)
        {
            await Shell.Current.GoToAsync(
                $"{nameof(ProductDetailPage)}?id={product.Id}");
        }
    }
}
