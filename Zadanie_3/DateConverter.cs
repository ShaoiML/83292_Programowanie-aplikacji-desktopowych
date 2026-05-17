using System.Globalization;
using System.Windows.Data;

namespace Z3_App;

/// <summary>
/// IValueConverter – konwertuje string ↔ DateTime? dla pola daty urodzin.
///
/// Convert:     DateTime? → string  (do wyświetlania w TextBox)
/// ConvertBack: string    → DateTime? (z TextBox do modelu)
///
/// Obsługuje formaty: DD.MM.RRRR  |  RRRR-MM-DD  |  DD/MM/RRRR
/// </summary>
public class DateConverter : IValueConverter
{
    private static readonly string[] Formats =
    [
        "dd.MM.yyyy",
        "yyyy-MM-dd",
        "dd/MM/yyyy"
    ];

    // DateTime? → string (wyświetlenie w polu tekstowym)
    public object? Convert(object? value, Type targetType,
                           object? parameter, CultureInfo culture)
    {
        if (value is DateTime dt)
            return dt.ToString("dd.MM.yyyy", culture);

        return string.Empty;
    }

    // string → DateTime? (przepisanie z pola tekstowego do modelu)
    public object? ConvertBack(object? value, Type targetType,
                               object? parameter, CultureInfo culture)
    {
        if (value is not string text || string.IsNullOrWhiteSpace(text))
            return null;

        if (DateTime.TryParseExact(text.Trim(), Formats,
                                   culture, DateTimeStyles.None, out var result))
            return result;

        // Niepoprawny format – zwróć null (binding nie zaktualizuje modelu)
        return null;
    }
}
