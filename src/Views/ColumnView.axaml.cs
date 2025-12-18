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

    private void EditColumnName(object? sender, PointerPressedEventArgs e) {
        if (DataContext is ColumnViewModel vm) { 
            vm.StartEditingCommand.Execute(null);
            TextBoxName.Focus();
        }
    }

    private void EnterKeyHit(object? sender, KeyEventArgs e) {
        if (e.Key == Key.Enter && DataContext is ColumnViewModel vm)
            vm.CommitColumnNameCommand.Execute(null);
    }

    private void NameLostFocus(object? sender, RoutedEventArgs e) {
        if (DataContext is ColumnViewModel vm)
            vm.CommitColumnNameCommand.Execute(null);
    }

    // COLUMN DRAGGING 

    // TODO: MIGHT NOT NEED THIS ANYMORE
    private void Column_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (DataContext is ColumnViewModel columnVm) {
            // Creating a static object drag manager from helpers to use new avalonia DataTransfer api
            DragManager.DraggedItem = columnVm;
            DragDrop.DoDragDropAsync(e, new DataTransfer(), DragDropEffects.Move);
        }
    }

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
            // Console.Write(sourceColumn.Tasks.IndexOf(taskVm)+ "\n");
            sourceColumn.MoveTask(taskVm, insertIndex);
        }
        else {
            // Inter-column move
            sourceColumn.RemoveTask(taskVm);
            targetColumn.InsertTask(taskVm, insertIndex);
        }

        DragManager.DraggedItem = null;
    }

    private int CalculateInsertIndex(ItemsControl itemsControl, Avalonia.Point point) {
        int count = itemsControl.ItemCount;

        for (int i = 0; i < count; i++) {
            var container = itemsControl.ContainerFromIndex(i);

            if (container is Control c) {
                var bounds = c.Bounds;

                if (point.Y < bounds.Top + bounds.Height) {
                    // Console.Write(i);
                    return i;
                }
            }
        }
        return count; // insert at end
    }
}
