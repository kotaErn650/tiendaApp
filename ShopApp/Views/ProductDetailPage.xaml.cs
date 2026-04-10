using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp.Views;

/// <summary>
/// Página de detalle de producto. Recibe el ID via IQueryAttributable.
/// </summary>
[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductDetailPage : ContentPage, IQueryAttributable
{
    private readonly ShopDbContext _dbContext;
    private Product? _product;

    public int ProductId { get; set; }

    public ProductDetailPage(ShopDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
    }

    /// <summary>
    /// Captura los parámetros de navegación y carga el producto correspondiente.
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("productId", out var idObj) &&
            int.TryParse(idObj?.ToString(), out int id))
        {
            ProductId = id;
            LoadProduct(id);
        }
    }

    private void LoadProduct(int id)
    {
        _product = _dbContext.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == id);

        if (_product is not null)
        {
            BindingContext = _product;
        }
        else
        {
            DisplayAlert("Error", "Producto no encontrado.", "OK");
        }
    }

    private void OnQuantityStepperChanged(object sender, ValueChangedEventArgs e)
    {
        LblQuantity.Text = ((int)e.NewValue).ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_product is null) return;

        try
        {
            _dbContext.Products.Update(_product);
            await _dbContext.SaveChangesAsync();
            await DisplayAlert("Éxito", "Producto actualizado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo guardar: {ex.Message}", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
