using System.Globalization;
using System.Windows.Data;

namespace Z3_App;

public class DateConverter : IValueConverter
{
    private static readonly string[] Formats =
    [
        "dd.MM.yyyy",
        "yyyy-MM-dd",
        "dd/MM/yyyy"
    ];


    public object? Convert(object? value, Type targetType,
                           object? parameter, CultureInfo culture)
    {
        if (value is DateTime dt)
            return dt.ToString("dd.MM.yyyy", culture);

        return string.Empty;
    }

  
    public object? ConvertBack(object? value, Type targetType,
                               object? parameter, CultureInfo culture)
    {
        if (value is not string text || string.IsNullOrWhiteSpace(text))
            return null;

        if (DateTime.TryParseExact(text.Trim(), Formats,
                                   culture, DateTimeStyles.None, out var result))
            return result;

        return null;
    }
}
