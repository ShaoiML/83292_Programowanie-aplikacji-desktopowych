using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Z3_App;

/// <summary>
/// Model osoby implementujący INotifyPropertyChanged.
/// Każda zmiana właściwości automatycznie powiadamia widok (data binding WPF).
/// </summary>
public class OsobaModel : INotifyPropertyChanged
{
    // ── Zdarzenie wymagane przez INotifyPropertyChanged ───────────────────────
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Pomocnicza metoda – odpala PropertyChanged dla podanej właściwości.
    /// [CallerMemberName] automatycznie wstawia nazwę wywołującej właściwości.
    /// </summary>
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    // ── Pola prywatne ─────────────────────────────────────────────────────────
    private string   _imieNazwisko  = string.Empty;
    private DateTime? _dataUrodzin  = null;
    private string   _pierwszeImie  = string.Empty;
    private string[] _imiona        = [];
    private string   _nazwisko      = string.Empty;
    private int?     _wiek          = null;

    // ═════════════════════════════════════════════════════════════════════════
    //  ImieNazwisko – setter rozbija napis na imiona[] + nazwisko
    // ═════════════════════════════════════════════════════════════════════════
    public string ImieNazwisko
    {
        get => _imieNazwisko;
        set
        {
            _imieNazwisko = value;

            // Rozbij względem spacji, usuń puste tokeny
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
                // tylko jedno słowo – traktuj jako imię
                Imiona       = parts;
                PierwszeImie = parts[0];
                Nazwisko     = string.Empty;
            }
            else
            {
                // ostatnie słowo → nazwisko, wszystko przed → imiona
                Nazwisko     = parts[^1];
                Imiona       = parts[..^1];
                PierwszeImie = parts[0];
            }

            OnPropertyChanged();   // powiadom binding o zmianie ImięNazwisko
        }
    }

    // ═════════════════════════════════════════════════════════════════════════
    //  dataUrodzin – setter oblicza i aktualizuje wiek
    // ═════════════════════════════════════════════════════════════════════════
    public DateTime? DataUrodzin
    {
        get => _dataUrodzin;
        set
        {
            _dataUrodzin = value;
            ObliczWiek();          // aktualizuje Wiek → ten odpala swój OnPropertyChanged
            OnPropertyChanged();
        }
    }

    // ═════════════════════════════════════════════════════════════════════════
    //  Wiek – ustawiany tylko przez ObliczWiek(), powiadamia widok
    // ═════════════════════════════════════════════════════════════════════════
    public int? Wiek
    {
        get => _wiek;
        private set
        {
            _wiek = value;
            OnPropertyChanged();   // aktualizuje TextBlock z wiekiem
        }
    }

    // ═════════════════════════════════════════════════════════════════════════
    //  Właściwości pomocnicze (każda powiadamia widok)
    // ═════════════════════════════════════════════════════════════════════════
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

    // ── Obliczanie wieku ──────────────────────────────────────────────────────
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

        // cofnij o 1 jeśli urodziny w tym roku jeszcze nie minęły
        if (urodziny.AddYears(wiek) > dzisiaj)
            wiek--;

        Wiek = wiek;
    }
}
