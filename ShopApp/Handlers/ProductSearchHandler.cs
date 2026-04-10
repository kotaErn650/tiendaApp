using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp.Handlers;

/// <summary>
/// SearchHandler personalizado para filtrar productos por nombre o descripción.
/// </summary>
public class ProductSearchHandler : SearchHandler
{
    private readonly ShopDbContext _dbContext;

    public ProductSearchHandler()
    {
        // Resolve DbContext from the DI container to honour single-instance semantics
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
            // Close the search results
            var searchPage = Shell.Current.CurrentPage;
            await Shell.Current.GoToAsync($"productdetail?productId={product.Id}");
        }
    }
}
