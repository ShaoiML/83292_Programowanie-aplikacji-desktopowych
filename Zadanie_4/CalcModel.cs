using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Z4_App;

public class CalcModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void Notify([CallerMemberName] string? n = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));

    private double? _left     = null;
    private double? _right    = null;
    private string? _op       = null;
    private bool    _newInput = true;
    private bool    _error    = false;

    private string _display   = "0";
    private string _opDisplay = string.Empty;

    public string Display
    {
        get => _display;
        private set { _display = value; Notify(); }
    }

    public string OpDisplay
    {
        get => _opDisplay;
        private set { _opDisplay = value; Notify(); }
    }


    public void Digit(string ch)
    {
        if (_error) return;

        if (_newInput)
        {
            Display   = ch == "." ? "0." : ch;
            _newInput = false;
        }
        else
        {
            if (ch == "." && Display.Contains('.')) return;
            Display = (Display == "0" && ch != ".") ? ch : Display + ch;
        }
    }

    public void ToggleSign()
    {
        if (_error || !double.TryParse(Display, out double v)) return;
        Display = Format(-v);
    }

    public void Backspace()
    {
        if (_error || _newInput) return;
        Display = Display.Length > 1 ? Display[..^1] : "0";
    }

    public void SetOp(string op)
    {
        if (_error || !double.TryParse(Display, out double val)) return;


        if (_op is not null && !_newInput)
        {
            _right = val;
            ExecuteBinary();
        }
        else
        {
            _left = val;
        }

        _op       = op;
        _newInput = true;
        OpDisplay = $"{Format(_left ?? 0)}  {op}";
    }


    public void Equals()
    {
        if (_error || _op is null) return;
        if (!double.TryParse(Display, out double val)) return;

      
        if (!_newInput)
            _right = val;
     

        ExecuteBinary();
        _newInput = true;
        OpDisplay = string.Empty;
    }

    private void ExecuteBinary()
    {
        double L = _left  ?? 0;
        double R = _right ?? 0;

        try
        {
            double result = _op switch
            {
                "+"   => L + R,
                "-"   => L - R,
                "×"   => L * R,
                "÷"   => R == 0 ? throw new DivideByZeroException() : L / R,
                "%+"  => L + L * R / 100,
                "%-"  => L - L * R / 100,
                "%×"  => L * R / 100,
                "%÷"  => R == 0 ? throw new DivideByZeroException() : L / (R / 100),
                "x^y" => Math.Pow(L, R),
                _     => L
            };

            _left   = result;
            Display = Format(result);
        }
        catch (DivideByZeroException)
        {
            Display = "Błąd: dzielenie przez 0";
            _error  = true;
        }
        catch
        {
            Display = "Błąd";
            _error  = true;
        }
    }

    public void Unary(string op)
    {
        if (_error || !double.TryParse(Display, out double val)) return;

        try
        {
            double result = op switch
            {
                "√"   => val < 0
                            ? throw new ArithmeticException("√ ujemnej")
                            : Math.Sqrt(val),
                "1/x" => val == 0
                            ? throw new DivideByZeroException()
                            : 1.0 / val,
                "x²"  => val * val,
                _     => val
            };

            _left     = result;
            Display   = Format(result);
            _newInput = true;
        }
        catch (DivideByZeroException)
        {
            Display = "Błąd: dzielenie przez 0";
            _error  = true;
        }
        catch (ArithmeticException ex)
        {
            Display = $"Błąd: {ex.Message}";
            _error  = true;
        }
    }

    public void Reset()
    {
        _left     = null;
        _right    = null;
        _op       = null;
        _newInput = true;
        _error    = false;
        Display   = "0";
        OpDisplay = string.Empty;
    }

    // ── Formatowanie liczby ───────────────────────────────────────────────────
    private static string Format(double v)
    {
        if (v == Math.Floor(v) && Math.Abs(v) < 1e15)
            return ((long)v).ToString();
        return v.ToString("G10");
    }
}
