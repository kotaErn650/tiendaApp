using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAcces;
using ShopApp.Models;

namespace ShopApp.Views;

/// <summary>
/// Página de detalle de producto. Recibe el ID via IQueryAttributable con parámetro "id".
/// </summary>
public partial class ProductDetailPage : ContentPage, IQueryAttributable
{
    private readonly ShopDbContext _dbContext;
    private List<Product> _allProducts = new();
    private Product? _product;
    private int _quantity = 1;

    public ProductDetailPage(ShopDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        EnsureProductsLoaded();
    }

    private void EnsureProductsLoaded()
    {
        if (_allProducts.Count != 0) return;

        _allProducts = _dbContext.Products.AsNoTracking().Include(p => p.Category).ToList();
        ProductPicker.ItemsSource = _allProducts.Select(p => p.Nombre).ToList();
    }

    /// <summary>
    /// Captura el parámetro "id" de la navegación y carga el producto.
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idObj) &&
            int.TryParse(idObj?.ToString(), out int id))
        {
            EnsureProductsLoaded();

            var index = _allProducts.FindIndex(p => p.Id == id);
            if (index >= 0)
            {
                ProductPicker.SelectedIndex = index;
            }
        }
    }

    private void OnProductPickerChanged(object sender, EventArgs e)
    {
        var index = ProductPicker.SelectedIndex;
        if (index < 0 || index >= _allProducts.Count) return;

        _product = _allProducts[index];
        BindingContext = _product;
        UpdateTotal();
    }

    private void OnQuantityStepperChanged(object sender, ValueChangedEventArgs e)
    {
        _quantity = (int)e.NewValue;
        LblQuantity.Text = _quantity.ToString();
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        if (_product is null) return;
        var total = _product.Precio * _quantity;
        LblTotal.Text = $"Total: {total:C}";
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}

