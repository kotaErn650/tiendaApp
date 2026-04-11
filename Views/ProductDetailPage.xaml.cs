using ShopApp.DataAcces;
using ShopApp.Models;

namespace ShopApp.Views;

[QueryProperty(nameof(ProductId), "id")]
public partial class ProductDetailPage : ContentPage, IQueryAttributable
{
    private readonly ShopDbContext _db;

    private int _productId;
    public int ProductId
    {
        get => _productId;
        set
        {
            _productId = value;
            LoadProduct(value);
        }
    }

    private Product? _product;
    public Product? SelectedProduct
    {
        get => _product;
        private set
        {
            _product = value;
            OnPropertyChanged();
        }
    }

    private int _cantidad = 1;
    public int Cantidad
    {
        get => _cantidad;
        set
        {
            _cantidad = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Total));
        }
    }

    public decimal Total => (SelectedProduct?.Precio ?? 0) * Cantidad;

    public ProductDetailPage(ShopDbContext db)
    {
        InitializeComponent();
        _db = db;
        BindingContext = this;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var val) && int.TryParse(val?.ToString(), out var id))
            LoadProduct(id);
    }

    private void LoadProduct(int id)
    {
        SelectedProduct = _db.Products.Find(id);
    }
}
