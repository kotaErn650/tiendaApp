using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopApp;

/// <summary>
/// Clase base que implementa INotifyPropertyChanged para ser usada como base de ViewModels o modelos enlazables.
/// </summary>
public abstract class BindingUtilObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        RaisePropertyChanged(propertyName);
        return true;
    }
}
