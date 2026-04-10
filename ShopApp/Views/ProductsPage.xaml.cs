using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAcces;
using ShopApp.Models;

namespace ShopApp.Views;

public partial class ProductsPage : ContentPage
{
    private readonly ShopDbContext _dbContext;
    private ObservableCollection<Product> _products = new();
    private List<Category> _categories = new();

    public ProductsPage(ShopDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadCategories();
        LoadProducts();
    }

    private void LoadCategories()
    {
        _categories = _dbContext.Categories.AsNoTracking().ToList();

        var pickerItems = new List<string> { "Todas" };
        pickerItems.AddRange(_categories.Select(c => c.Nombre));

        CategoryPicker.ItemsSource = pickerItems;
        CategoryPicker.SelectedIndex = 0;
    }

    private void LoadProducts(int? categoryId = null)
    {
        IQueryable<Product> query = _dbContext.Products.AsNoTracking().Include(p => p.Category);

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        _products = new ObservableCollection<Product>(query.ToList());
        ProductsCollectionView.ItemsSource = _products;
    }

    private void OnCategoryFilterChanged(object sender, EventArgs e)
    {
        int selectedIndex = CategoryPicker.SelectedIndex;

        if (selectedIndex <= 0)
        {
            LoadProducts();
        }
        else
        {
            var category = _categories[selectedIndex - 1];
            LoadProducts(category.Id);
        }
    }

    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Product product)
        {
            ProductsCollectionView.SelectedItem = null;
            await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?id={product.Id}");
        }
    }

    private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (sender is Stepper stepper)
        {
            var parent = stepper.Parent as Grid;
            if (parent?.Children.OfType<Label>().LastOrDefault() is Label label)
            {
                label.Text = ((int)e.NewValue).ToString();
            }
        }
    }
}

