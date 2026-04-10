using Microsoft.EntityFrameworkCore;
using ShopApp.DataAcces;
using ShopApp.Models;
using ShopApp.Views;

namespace ShopApp.Handlers;

/// <summary>
/// SearchHandler personalizado que filtra productos por nombre y navega al detalle al seleccionar.
/// </summary>
public class ProductoBusquedaHandler : SearchHandler
{
    private readonly ShopDbContext _dbContext;

    public ProductoBusquedaHandler()
    {
        _dbContext = IPlatformApplication.Current!.Services.GetRequiredService<ShopDbContext>();
        ItemTemplate = (DataTemplate)Application.Current!.Resources["ProductDataTemplate"];
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
            var query = newValue.Trim().ToLower();
            var results = _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Nombre.ToLower().Contains(query) ||
                            p.Descripcion.ToLower().Contains(query))
                .ToList();

            ItemsSource = results;
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        if (item is Product product)
        {
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={product.Id}");
        }
    }
}
