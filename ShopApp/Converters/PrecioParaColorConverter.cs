using System.Globalization;

namespace ShopApp.Converters;

/// <summary>
/// Convierte un precio decimal a un color:
/// - Precio ≤ 100 → LimeGreen
/// - Precio > 100 → DarkViolet
/// </summary>
public class PrecioParaColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal precio)
        {
            return precio <= 100m ? Colors.LimeGreen : Colors.DarkViolet;
        }

        return Colors.Gray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
