using Avalonia.Controls;
using App.Views;

namespace src;

public partial class MainWindow : Window {

    public MainWindow() {
        InitializeComponent();
        this.DataContext = new MainWindowView();        
    }
}
