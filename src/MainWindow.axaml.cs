// TODO: Currently I save the board to json on load so that it resaves the loaded 
//       state. This seems broken, and also on a project blow up it wipes the json 
//       fix

using Avalonia.Controls;
using Avalonia.Visuals;
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
        TaskBoard = JsonDB.LoadBoard();
        InitializeComponent();

        // TODO: These two handlers define taskcard dragging, update to newer way
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

            // Remove the column from the board 
            if (result) {
                col.ParentBoard?.RemoveColumn(col);
            }
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

            JsonDB.SaveBoard(TaskBoard);
        }
    }

    private void TextBox_KeyDown(object? sender, KeyEventArgs e) {
        if (e.Key == Key.Enter && sender is TextBox tb && tb.DataContext is ColumnManager col) {
            col.IsEditing = false;
            e.Handled = true;

            JsonDB.SaveBoard(TaskBoard);
        }
    }

    // -------------- CODE PRETAINING TO TASKCARDS SPECIFICALLY ----------
    // Code pretaining to the taskcard editing in the main window 
    // TODO: The below is for dragging and dropping taskcards in the UI and needs 
    //       a full rewrite once we get it up and running... this will get temporarily
    //       nasty looking 
    
    private TaskCard? _draggingCard;
    private ColumnManager? _dragSourceColumn;

    /* Handles title of card clicks to open up edit popup*/
    private async void TaskName_PointerPressed(object? sender, PointerPressedEventArgs e) {

        e.Handled = true;

        if (sender is not Control control) 
            return;

        if (control.DataContext is not TaskCard task) 
            return;

        var popup = new TaskPopup(task);
        var result = await popup.ShowDialog<bool>(this);

        // TODO: MOVE? Needed to update taskcard title and colour (and everything else)
        if (result)
            JsonDB.SaveBoard(TaskBoard);
    }

    /* Mostly handles the starting of a click and drag on a task card*/
    private async void TaskCard_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            return;
        if (sender is not Control control) return;

        if (control.DataContext is not TaskCard task) return;

        var ancestor = control.GetVisualAncestors()
                              .OfType<Control>()
                              .FirstOrDefault(c => c.DataContext is ColumnManager);

        var sourceColumn = ancestor?.DataContext as ColumnManager;

        _draggingCard = task;
        _dragSourceColumn = sourceColumn;

        // TODO: Change to DataTranfer() once fully working
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
    /*  Keeps track of the item being drag for UI feedback*/
    private void OnDragOver(object? sender, DragEventArgs e) {
        if (e.Data.Contains("task-card"))
            e.DragEffects = DragDropEffects.Move;
        else
            e.DragEffects = DragDropEffects.None;

        e.Handled = true;
    }

    /* Handles the dropping of a taskcard after its been clicked and dragged*/
    private void OnDrop(object? sender, DragEventArgs e) {
        
        if (!e.Data.Contains("task-card")) return;
        // the task card we are dropping 
        var dropped = e.Data.Get("task-card") as TaskCard;
        if (dropped == null) return;

        var control = e.Source as Control;
        // Find the column underneath the mouse
        var columnControl = control?
            .GetVisualAncestors()
            .OfType<Control>()
            .FirstOrDefault(c => c.DataContext is ColumnManager);

        if (columnControl?.DataContext is not ColumnManager targetColumn)
            return;

        // Find the task under the mouse if there is one 
        var taskControl = control?
            .GetVisualAncestors()
            .OfType<Control>()
            .FirstOrDefault(c => c.DataContext is TaskCard);

        var targetTask = taskControl?.DataContext as TaskCard;
        int insertIdx = targetColumn.TaskList.Count;

        if (targetTask != null) 
            insertIdx = targetColumn.TaskList.IndexOf(targetTask);

        // If the card is dropped in the same column, reorder accordingly
        if (_dragSourceColumn == targetColumn && targetTask != null) {
            var oldIdx = targetColumn.TaskList.IndexOf(dropped);

            if (oldIdx == insertIdx) 
                return;
            targetColumn.MoveTask(dropped, insertIdx);
        }
        _dragSourceColumn?.RemoveTask(dropped);
        targetColumn.InsertTask(dropped, insertIdx);

        e.Handled = true;
    }
}
