using Microsoft.EntityFrameworkCore;
using ShopApp.DataAcces;
using ShopApp.Models;

namespace ShopApp.Views;

/// <summary>
/// Lightweight display wrapper used to expose a computed initial for the avatar.
/// </summary>
internal sealed class ClientDisplayItem
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public string Inicial => Nombre.Length > 0 ? Nombre[0].ToString().ToUpper() : "?";

    public static ClientDisplayItem FromClient(Client c) =>
        new() { Id = c.Id, Nombre = c.Nombre, Direccion = c.Direccion };
}

public partial class ClientsPage : ContentPage
{
    private readonly ShopDbContext _dbContext;

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

    private void LoadClients()
    {
        var clients = _dbContext.Clients
            .AsNoTracking()
            .OrderBy(c => c.Nombre)
            .Select(c => ClientDisplayItem.FromClient(c))
            .ToList();

        ClientsCollectionView.ItemsSource = clients;
        LblCount.Text = $"{clients.Count} cliente{(clients.Count != 1 ? "s" : "")}";
    }
}
