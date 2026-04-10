using Microsoft.EntityFrameworkCore;
using ShopApp.Data;

namespace ShopApp.Views;

public partial class SummaryPage : ContentPage
{
    private readonly ShopDbContext _dbContext;

    public SummaryPage(ShopDbContext dbContext)
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
        LblTotalProducts.Text = _dbContext.Products.Count().ToString();
        LblTotalClients.Text = _dbContext.Clients.Count().ToString();
        LblTotalCategories.Text = _dbContext.Categories.Count().ToString();

        var avgPrice = _dbContext.Products.Any()
            ? _dbContext.Products.Average(p => p.Precio)
            : 0;
        LblAvgPrice.Text = $"${avgPrice:N2}";
    }

    private async void OnVerProductosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//products");
    }

    private async void OnVerClientesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//clients");
    }
}
