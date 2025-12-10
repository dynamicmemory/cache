using Avalonia.Controls;
using App.ViewModels;

namespace App.Views;

public partial class MainView : UserControl {

    public MainView() {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

}
