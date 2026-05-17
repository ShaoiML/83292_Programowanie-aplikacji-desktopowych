using System.Windows;

namespace Z3_App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Ustawienie DataContext – wszystkie Binding w XAML będą szukać
        // właściwości właśnie w tym obiekcie
        DataContext = new OsobaModel();
    }
}
