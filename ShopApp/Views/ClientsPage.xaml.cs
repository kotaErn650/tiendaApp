using System.Collections.ObjectModel;
using ShopApp.Data;
using ShopApp.Models;

namespace ShopApp.Views;

public partial class ClientsPage : ContentPage
{
    private readonly ShopDbContext _dbContext;
    private ObservableCollection<Client> _clients = new();

    public ClientsPage(ShopDbContext dbContext)
    {
        InitializeComponent();
        _dbContext = dbContext;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadClients();
    }

    private void LoadClients(string? filter = null)
    {
        IQueryable<Client> query = _dbContext.Clients.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            var lower = filter.ToLower();
            query = query.Where(c =>
                c.Nombre.ToLower().Contains(lower) ||
                c.Direccion.ToLower().Contains(lower));
        }

        _clients = new ObservableCollection<Client>(query.ToList());
        ClientsCollectionView.ItemsSource = _clients;
        LblClientCount.Text = $"Total: {_clients.Count} clientes";
    }

    private void OnClientSearchChanged(object sender, TextChangedEventArgs e)
    {
        LoadClients(e.NewTextValue);
    }

    private async void OnNewClientClicked(object sender, EventArgs e)
    {
        string? nombre = await DisplayPromptAsync("Nuevo Cliente", "Nombre del cliente:");
        if (string.IsNullOrWhiteSpace(nombre)) return;

        string? direccion = await DisplayPromptAsync("Nuevo Cliente", "Dirección:");

        var newClient = new Client
        {
            Nombre = nombre.Trim(),
            Direccion = direccion?.Trim() ?? string.Empty
        };

        _dbContext.Clients.Add(newClient);
        await _dbContext.SaveChangesAsync();
        LoadClients();
    }
}
