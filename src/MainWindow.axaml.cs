using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.VisualTree;
using App.Board;
using App.Managers;
using App.Models;
using App.Views;
using System.Linq;
using System;

namespace src;

public partial class MainWindow : Window {

    public Board TaskBoard { get; }

    public MainWindow() {
        TaskBoard = new Board();
        InitializeComponent();
        AddHandler(DragDrop.DragOverEvent, OnDragOver);
        AddHandler(DragDrop.DropEvent, OnDrop);

        this.DataContext = TaskBoard;
    }

    // ------- CODE PRETAINING TO COLUMNS SPECIFICALLY -------------------
    /* Handles the removal of a column by passing the column to the parent board 
     * which removes the column from its observable list */
    private async void RemoveColumn_Click(object sender, RoutedEventArgs e) {

        if (sender is Button btn && btn.DataContext is ColumnManager col) {

            if (col == col.ParentBoard?.FirstColumn) return;

            var dialog = new ConfirmDialog($"Delete column '{col.ColName}'?");
            var result = await dialog.ShowDialog<bool>(this);

            if (result) col.ParentBoard?.RemoveColumn(col);
        }
    }

    private void TextBlock_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (sender is TextBlock tb && 
            tb.DataContext is ColumnManager col && 
            col.NameEditable) col.IsEditing = true;
    }

    private void TextBox_LostFocus(object? sender, RoutedEventArgs e) {
        if (sender is TextBox tb && tb.DataContext is ColumnManager col) {
            col.IsEditing = false;
        }
    }

    private void TextBox_KeyDown(object? sender, KeyEventArgs e) {
        if (e.Key == Key.Enter && sender is TextBox tb && tb.DataContext is ColumnManager col) {
            col.IsEditing = false;
            e.Handled = true;
        }
    }

    // -------------- CODE PRETAINING TO TASKCARDS SPECIFICALLY ----------
    // Code pretaining to the taskcard editing in the main window 
    // TODO: The below is for dragging and dropping taskcards in the UI and needs 
    //       a full rewrite once we get it up and running... this will get temporarily
    //       nasty looking 
    
    private TaskCard? _draggingCard;
    private ColumnManager? _dragSourceColumn;
    private Border? _dragVisual;


    private async void TaskCard_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            return;
        if (sender is not Avalonia.Controls.Control control) return;

        if (control.DataContext is not TaskCard task) return;

        var ancestor = control.GetVisualAncestors()
            .OfType<Avalonia.Controls.Control>()
            .FirstOrDefault(c => c.DataContext is ColumnManager);

        var sourceColumn = ancestor?.DataContext as ColumnManager;

        _draggingCard = task;
        _dragSourceColumn = sourceColumn;

        var data = new DataObject();
        data.Set("task-card", task);

        try {
            await DragDrop.DoDragDrop(e, data, DragDropEffects.Move);
        }
        finally {
            // clear local dragging state after drag done
            _draggingCard = null;
            _dragSourceColumn = null;
        }
    }

    private void OnDragOver(object? sender, DragEventArgs e) {
        if (e.Data.Contains("task-card"))
            e.DragEffects = DragDropEffects.Move;
        else
            e.DragEffects = DragDropEffects.None;

        e.Handled = true;
    }


    private void OnDrop(object? sender, DragEventArgs e) {
        if (!e.Data.Contains("task-card")) return;

        var dropped = e.Data.Get("task-card") as TaskCard;
        if (dropped == null) return;

        var control = e.Source as Control;
        var columnControl = control?
            .GetVisualAncestors()
            .OfType<Control>()
            .FirstOrDefault(c => c.DataContext is ColumnManager);

        if (columnControl?.DataContext is not ColumnManager targetColumn)
            return;

        Console.WriteLine("DROP TARGET: " + targetColumn.ColName);

        // Same column
        if (_dragSourceColumn != null && _dragSourceColumn == targetColumn) {
            var currentIndex = targetColumn.TaskList.IndexOf(dropped);
            var lastIndex = targetColumn.TaskList.Count - 1;

            if (currentIndex != lastIndex) {
                targetColumn.MoveTask(dropped, lastIndex);
            }
        }
        else {
            _dragSourceColumn?.RemoveTask(dropped);
            targetColumn.InsertTask(dropped, targetColumn.TaskList.Count);
        }
        e.Handled = true;
    }
}
