using System.Globalization;

namespace ShopApp.Converters;

/// <summary>
/// Convierte el precio de un producto en un color: rojo para precios altos (>= 500),
/// naranja para precios medios (>= 100) y verde para precios bajos.
/// </summary>
public class PrecioParaColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal precio)
        {
            if (precio >= 500m)
                return Colors.Red;
            if (precio >= 100m)
                return Colors.Orange;
            return Colors.Green;
        }

        return Colors.Black;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
