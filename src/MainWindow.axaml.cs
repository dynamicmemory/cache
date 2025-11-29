using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia;
using Avalonia.VisualTree;
using App.Board;
using App.Managers;
using App.Models;
using App.Views;

namespace src;

public partial class MainWindow : Window {

    public Board TaskBoard { get; }

    public MainWindow() {
        TaskBoard = new Board();
        InitializeComponent();

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
    
    private async void TaskCard_Pressed(object? sender, PointerPressedEventArgs e) {
        if (sender is Border b && b.DataContext is TaskCard t) {
            var popup = new TaskPopup(t);
            await popup.ShowDialog<bool>(this);
        }
    }
    // TODO: ADD ENTER CONFIRMS TASKCARD CHANGES IN THE POPUP
}
