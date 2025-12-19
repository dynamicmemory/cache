/* */
using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using App.ViewModels;
using App.Helpers;

namespace App.Views;

public partial class ColumnView : UserControl {

    public ColumnView() {
        InitializeComponent();
    }

    // COLUMN HEADER EDITING

    /* Handles changing the column name to be editable and therefore switching 
     * the textblock into a textbox 
     */
    private void EditColumnName(object? sender, PointerPressedEventArgs e) {
        if (DataContext is ColumnViewModel vm) { 
            vm.StartEditingCommand.Execute(null);
            TextBoxName.Focus();
        }
    }

    /* Handles the case where a user uses "enter" to finish editing the title */
    private void EnterKeyHit(object? sender, KeyEventArgs e) {
        if (e.Key == Key.Enter && DataContext is ColumnViewModel vm)
            vm.CommitColumnNameCommand.Execute(null);
    }

    /* Handles the case where a user clicks away to finish editing the title */
    private void NameLostFocus(object? sender, RoutedEventArgs e) {
        if (DataContext is ColumnViewModel vm)
            vm.CommitColumnNameCommand.Execute(null);
    }

    /* Confirms if user wants to delete column, sends to remove column command 
     * if true 
     */
    private async void DeleteColumn_Click(object? sender, RoutedEventArgs e) {
        if (DataContext is ColumnViewModel vm) {
            var window = this.VisualRoot as Avalonia.Controls.Window;
            if (window == null) return;

            var popup = new ConfirmDialogView("This Column has tasks. Are you sure you want to remove it?");
            bool conf = await popup.ShowDialog<bool>(window);
            if (conf) vm.RemoveColumnCommand.Execute(null);
        }
    }

    // COLUMN DRAGGING 

  /* Handles the start of the column dragging*/
  private void ColumnDragHandle_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (DataContext is not ColumnViewModel columnVm)
            return;

        DragManager.DraggedItem = columnVm;
        e.Handled=true;
        DragDrop.DoDragDropAsync(e, new DataTransfer(), DragDropEffects.Move);
    }

    // TASK DRAGGING

    /* Add my animation stuff here during drag*/
    private void TasksList_DragOver(object? sender, DragEventArgs e) {
        e.DragEffects = DragDropEffects.Move;
    }

    /* Handles where a taskcard was dropped in a column */
    private void TasksList_Drop(object? sender, DragEventArgs e) {
        if (DragManager.DraggedItem is not (TaskCardViewModel taskVm, ColumnViewModel sourceColumn)) 
            return;

        // Find the control that holds the list of tasks as we are storing it 
        // in the parent scrollbar so we can insert into empty columns
        var itemsControl = this.FindControl<ItemsControl>("TaskList");
        if (itemsControl?.DataContext is not ColumnViewModel targetColumn)
            return;
    
        // Get the index of the drop zone in the column
        var point = e.GetPosition(itemsControl);
        int insertIndex = CalculateInsertIndex(itemsControl, point);

        if (sourceColumn == targetColumn) {
            // Single-column reordering
            sourceColumn.MoveTask(taskVm, insertIndex);
        }
        else {
            // Inter-column move
            sourceColumn.RemoveTask(taskVm);
            targetColumn.InsertTask(taskVm, insertIndex);
        }
        DragManager.DraggedItem = null;
    }

    /* Calculates the index of the object that was dragged and dropped */
    private int CalculateInsertIndex(ItemsControl itemsControl, Avalonia.Point point) {
        int count = itemsControl.ItemCount;

        for (int i = 0; i < count; i++) {
            var container = itemsControl.ContainerFromIndex(i);

            if (container is Control c) {
                var bounds = c.Bounds;

                if (point.Y < bounds.Top + bounds.Height) {
                    return i;
                }
            }
        }
        return count; // insert at end
    }
}
