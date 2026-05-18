using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Z3_App;

public class OsobaModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private string   _imieNazwisko  = string.Empty;
    private DateTime? _dataUrodzin  = null;
    private string   _pierwszeImie  = string.Empty;
    private string[] _imiona        = [];
    private string   _nazwisko      = string.Empty;
    private int?     _wiek          = null;

    public string ImieNazwisko
    {
        get => _imieNazwisko;
        set
        {
            _imieNazwisko = value;

            var parts = value.Trim()
                             .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                Imiona       = [];
                PierwszeImie = string.Empty;
                Nazwisko     = string.Empty;
            }
            else if (parts.Length == 1)
            {
                Imiona       = parts;
                PierwszeImie = parts[0];
                Nazwisko     = string.Empty;
            }
            else
            {
                Nazwisko     = parts[^1];
                Imiona       = parts[..^1];
                PierwszeImie = parts[0];
            }

            OnPropertyChanged();   
        }
    }

    public DateTime? DataUrodzin
    {
        get => _dataUrodzin;
        set
        {
            _dataUrodzin = value;
            ObliczWiek();   
            OnPropertyChanged();
        }
    }

    public int? Wiek
    {
        get => _wiek;
        private set
        {
            _wiek = value;
            OnPropertyChanged();   
        }
    }

    public string PierwszeImie
    {
        get => _pierwszeImie;
        private set { _pierwszeImie = value; OnPropertyChanged(); }
    }

    public string[] Imiona
    {
        get => _imiona;
        private set { _imiona = value; OnPropertyChanged(); }
    }

    public string Nazwisko
    {
        get => _nazwisko;
        private set { _nazwisko = value; OnPropertyChanged(); }
    }


    private void ObliczWiek()
    {
        if (_dataUrodzin is null)
        {
            Wiek = null;
            return;
        }

        var dzisiaj   = DateTime.Today;
        var urodziny  = _dataUrodzin.Value;
        int wiek      = dzisiaj.Year - urodziny.Year;

        if (urodziny.AddYears(wiek) > dzisiaj)
            wiek--;

        Wiek = wiek;
    }
}
