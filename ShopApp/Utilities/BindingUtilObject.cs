using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopApp.Utilities;

/// <summary>
/// Clase base que implementa INotifyPropertyChanged para facilitar el binding en MVVM.
/// </summary>
public class BindingUtilObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Notifica a la UI que una propiedad ha cambiado.
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Establece el valor de una propiedad y dispara la notificación de cambio si el valor es diferente.
    /// </summary>
    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
