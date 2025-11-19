using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using App.Board;
using App.Managers;

namespace src;

public partial class MainWindow : Window {

    public Board TaskBoard { get; }

    public MainWindow() {
        TaskBoard = new Board();
        InitializeComponent();

        this.DataContext = TaskBoard;
    }

    private void AddTask(object? sender, RoutedEventArgs e) {
        if (sender is Button btn) {
            var col = btn.FindAncestorOfType<ColumnManager>();
            if (col != null) {
                col.Manager.AddNewTask();
            }
        }
    }
}
