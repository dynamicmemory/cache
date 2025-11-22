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

    /* Handles the removal of a column by passing the column to the parent board 
     * which removes the column from its observable list */
    private void RemoveColumn_Click(object sender, RoutedEventArgs e) {
        if (sender is Button btn && btn.DataContext is ColumnManager col)
            col.Parent?.RemoveColumn(col);
    }
}
