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
    
    private TaskCard? _draggedTask;
    private ColumnManager? _sourceColumn;
    private Point _dragStart;
    private bool _isDragging = false;

    /* Handles detection for a taskcard being clicked, passes the card of to 
     * TaskCard_Released if user released the click, otherwise hands the card 
     * off to the TaskCard_Moved if user holds the click to drag the card*/
    private void TaskCard_Pressed(object? sender, PointerPressedEventArgs e) {
        if (sender is Border b && b.DataContext is TaskCard task) {
            _draggedTask = task;
            _sourceColumn = b.GetVisualParent<ItemsControl>()?.DataContext as ColumnManager;

            _dragStart = e.GetPosition(this);
            _isDragging = false;

            e.Pointer.Capture(this);

            this.PointerMoved += TaskCard_Moved;
            this.PointerReleased += TaskCard_Released;
        }
    }

    /* Handles the drapping logic for a taskcard*/
    private void TaskCard_Moved(object? sender, PointerEventArgs e) {
        if (_draggedTask == null) return;

        var pos = e.GetPosition(this);
        if (!_isDragging) {
            if ((Math.Abs(pos.X - _dragStart.X) > 5) || (Math.Abs(pos.Y - _dragStart.Y) > 5)) {
                _isDragging = true;
                Console.WriteLine($"Start dragging task: {_draggedTask.TaskName}");
            }
        }

        if (_isDragging) {
            Console.WriteLine($"Dragging at {pos.X}, {pos.Y}");
            // Animation for the dragging willl go here.
        }
    }

    /* Handles the releasing logic for a taskcard*/
    private async void TaskCard_Released(object? sender, PointerReleasedEventArgs e) {
        if (sender is Border b) {
            e.Pointer.Capture(null);
            this.PointerMoved -= TaskCard_Moved;
            this.PointerReleased -= TaskCard_Released;
        }

        if (!_isDragging && _draggedTask != null) {
            var popup = new TaskPopup(_draggedTask);
            await popup.ShowDialog<bool>(this);
        }
        else if (_isDragging && _draggedTask != null && _sourceColumn != null) {

            var releasePosition = e.GetPosition(this);
            var targetColumn = GetColumnAt(releasePosition);

            if (targetColumn != null) {
                // int insertIndex = CalculateInsertIndex(targetColumn, releasePosition);
                DropTask(_draggedTask, _sourceColumn, targetColumn);
            }
            // Drop logic will go here as well as moving between columns and reoder
            Console.WriteLine($"Dropped task {_draggedTask.TaskName}");
        }
        _draggedTask = null;
        _sourceColumn = null;
        _isDragging = false;
    }

    /* Get a column at a given x,y coord point on the UI*/
    private ColumnManager? GetColumnAt(Point position) {
        foreach (var col in TaskBoard.ColumnList) {
            var container = this.FindControl<ItemsControl>(col.ColName);
            var topLeft = container.TranslatePoint(new Point(0,0), this);

            if (topLeft != null) {
                var rect = new Rect(topLeft.Value, container.Bounds.Size);
                if (rect.Contains(position)) return col;
            }

        }
        return null;
    }

    /* Helper function for moving one task to another column or reordering*/
    private void DropTask(TaskCard task, ColumnManager? source, ColumnManager? target, int index = -1) {
        if (source == null || target == null) return;

        Console.WriteLine($"Moving '{task.TaskName}' from '{source.ColName}' {target.ColName}");

        source.Manager.Tasks.Remove(task);
        if (index < 0 || index > target.Manager.Tasks.Count) 
            target.Manager.Tasks.Add(task);
        else 
            target.Manager.Tasks.Insert(index, task);

        Console.WriteLine("Target tasks now:");
        foreach(var t in target.Manager.Tasks)
            Console.WriteLine($" - {t.TaskName}");
    }
    // TODO: ADD ENTER CONFIRMS TASKCARD CHANGES IN THE POPUP
}
