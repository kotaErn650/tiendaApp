using Microsoft.EntityFrameworkCore;
using ShopApp.DataAcces;

namespace ShopApp.Views;

public partial class MainPage : ContentPage
{
    private readonly ShopDbContext _dbContext;

    public MainPage(ShopDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadSummary();
    }

    private void LoadSummary()
    {
        LblTotalProducts.Text   = _dbContext.Products.Count().ToString();
        LblTotalClients.Text    = _dbContext.Clients.Count().ToString();
        LblTotalCategories.Text = _dbContext.Categories.Count().ToString();

        var avgPrice = _dbContext.Products.Any()
            ? _dbContext.Products.Average(p => p.Precio)
            : 0;
        LblAvgPrice.Text = avgPrice.ToString("C");
    }

    private async void OnVerProductosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//products");
    }
}
