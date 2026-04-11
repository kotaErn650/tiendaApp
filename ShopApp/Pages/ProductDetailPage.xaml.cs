using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess;

namespace ShopApp.Pages;

[QueryProperty(nameof(ProductId), "id")]
public partial class ProductDetailPage : ContentPage, IQueryAttributable
{
    private readonly ShopDbContext _db;
    private Product? _currentProduct;

    public string? ProductId { get; set; }

    public ProductDetailPage(ShopDbContext db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _db.Database.EnsureCreated();

        // Cargar todos los productos en el Picker
        var products = _db.Products.AsNoTracking().ToList();
        PickerProductos.ItemsSource = products;

        // Si llegó un id por parámetro, seleccionar el producto
        if (int.TryParse(ProductId, out int id))
        {
            var index = products.FindIndex(p => p.Id == id);
            if (index >= 0)
                PickerProductos.SelectedIndex = index;
        }
        else if (products.Count > 0)
        {
            PickerProductos.SelectedIndex = 0;
        }
    }

    // IQueryAttributable: recibe parámetros de la URL de navegación
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idValue))
        {
            ProductId = idValue?.ToString();
        }
    }

    private void OnPickerProductoChanged(object sender, EventArgs e)
    {
        if (PickerProductos.SelectedItem is Product selected)
        {
            _currentProduct = selected;
            UpdateUI();
        }
    }

    private void OnStepperChanged(object sender, ValueChangedEventArgs e)
    {
        LabelCantidad.Text = ((int)e.NewValue).ToString();
        CalcularTotal();
    }

    private void UpdateUI()
    {
        if (_currentProduct is null) return;

        LabelNombre.Text      = _currentProduct.Nombre;
        LabelDescripcion.Text = _currentProduct.Descripcion;
        LabelCategoria.Text   = _currentProduct.CategoryId.ToString();

        LabelPrecio.Text = _currentProduct.Precio.ToString("C");
        LabelPrecio.TextColor = _currentProduct.Precio <= 100m
            ? Colors.LimeGreen
            : Colors.DarkViolet;

        CalcularTotal();
    }

    private void CalcularTotal()
    {
        if (_currentProduct is null) return;
        var cantidad = (int)StepperCantidad.Value;
        LabelTotal.Text = (_currentProduct.Precio * cantidad).ToString("C");
    }

    private async void OnAgregarCarritoClicked(object sender, EventArgs e)
    {
        if (_currentProduct is null) return;
        var cantidad = (int)StepperCantidad.Value;
        await DisplayAlert(
            "Carrito",
            $"Se agregaron {cantidad} unidad(es) de '{_currentProduct.Nombre}' al carrito.\nTotal: {(_currentProduct.Precio * cantidad):C}",
            "OK");
    }
}
