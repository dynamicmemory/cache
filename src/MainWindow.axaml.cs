using Avalonia.Controls;
using App.Board;

namespace src;

public partial class MainWindow : Window {

    public Board TaskBoard { get; }

    public MainWindow() {
        TaskBoard = new Board();
        InitializeComponent();

        this.DataContext = TaskBoard;
    }
}
