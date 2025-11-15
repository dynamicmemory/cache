using Avalonia.Controls;
using App.Managers;
namespace src;

public partial class MainWindow : Window {

    private TaskManager manager; 

    public MainWindow() {
        InitializeComponent();
        manager = new TaskManager();
        manager.GetTask();

        this.DataContext = manager;
    }
}
