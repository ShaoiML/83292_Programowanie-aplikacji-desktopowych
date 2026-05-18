using System.Windows;

namespace Z3_App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new OsobaModel();
    }
}
