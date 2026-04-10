using System.Globalization;

namespace ShopApp.Converters;

/// <summary>
/// Convierte el precio de un producto en un color:
/// LimeGreen para precios ≤ 100 y DarkViolet para precios > 100.
/// </summary>
public class PrecioParaColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal precio)
        {
            return precio <= 100m ? Colors.LimeGreen : Colors.DarkViolet;
        }

        return Colors.Black;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
