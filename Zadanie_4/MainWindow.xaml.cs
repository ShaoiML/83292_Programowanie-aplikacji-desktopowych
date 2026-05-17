using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Z4_App;

public partial class MainWindow : Window
{
    private readonly CalcModel _model = new();

    public MainWindow()
    {
        InitializeComponent();
        // Binding działa przez DataContext
        DataContext = _model;
    }

    // ── Cyfry ─────────────────────────────────────────────────────────────────
    private void Digit_Click(object sender, RoutedEventArgs e)
    {
        _model.Digit(((Button)sender).Content.ToString()!);
        UpdateDisplayStyle();
    }

    private void Dot_Click(object sender, RoutedEventArgs e)
    {
        _model.Digit(".");
        UpdateDisplayStyle();
    }

    // ── Operacje binarne ──────────────────────────────────────────────────────
    private void Op_Click(object sender, RoutedEventArgs e)
    {
        // Tag zawiera wewnętrzny symbol operacji (np. "×" → model używa "×")
        var op = ((Button)sender).Tag?.ToString()
              ?? ((Button)sender).Content.ToString()!;
        _model.SetOp(op);
        UpdateDisplayStyle();
    }

    // ── Operacje jednoargumentowe ─────────────────────────────────────────────
    private void Unary_Click(object sender, RoutedEventArgs e)
    {
        var op = ((Button)sender).Tag?.ToString()
              ?? ((Button)sender).Content.ToString()!;
        _model.Unary(op);
        UpdateDisplayStyle();
    }

    // ── Równa się ─────────────────────────────────────────────────────────────
    private void Equals_Click(object sender, RoutedEventArgs e)
    {
        _model.Equals();
        UpdateDisplayStyle();
    }

    // ── Specjalne ─────────────────────────────────────────────────────────────
    private void C_Click(object sender, RoutedEventArgs e)
    {
        _model.Reset();
        UpdateDisplayStyle();
    }

    private void Sign_Click(object sender, RoutedEventArgs e)
    {
        _model.ToggleSign();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        _model.Backspace();
    }

    // ── Kolor wyświetlacza przy błędzie ───────────────────────────────────────
    private void UpdateDisplayStyle()
    {
        bool isError = _model.Display.StartsWith("Błąd");
        Display.Foreground = isError
            ? new SolidColorBrush(Color.FromRgb(0xf3, 0x8b, 0xa8))  // czerwony
            : new SolidColorBrush(Color.FromRgb(0xcd, 0xd6, 0xf4)); // normalny
    }
}
