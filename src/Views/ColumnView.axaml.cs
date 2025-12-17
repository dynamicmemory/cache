using System.Windows;
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

    private void Column_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (DataContext is ColumnViewModel columnVm) {
            // Creating a static object drag manager from helpers to use new avalonia DataTransfer api
            DragManager.DraggedItem = columnVm;
            DragDrop.DoDragDropAsync(e, new DataTransfer(), DragDropEffects.Move);
        }
    }

    // TASK DRAGGING

    /* Add my animation stuff here during drag*/
    private void TasksList_DragOver(object? sender, DragEventArgs e) {
        e.DragEffects = DragDropEffects.Move;
    }

    private void TasksList_Drop(object? sender, DragEventArgs e) {
        if (DragManager.DraggedItem is not (TaskCardViewModel taskVm, ColumnViewModel sourceColumn)) 
            return;

        if (sender is ItemsControl itemsControl && itemsControl.DataContext is ColumnViewModel targetColumn) {
            int insertIndex = CalculateInsertIndex(itemsControl, e.GetPosition(itemsControl));

            if (sourceColumn == targetColumn) {
                // Single-column reordering
                sourceColumn.MoveTask(taskVm, insertIndex);
            }
            else {
                // Inter-column move
                sourceColumn.RemoveTask(taskVm);
                targetColumn.InsertTask(taskVm, insertIndex);
            }
        }
        DragManager.DraggedItem = null;
    }

    private int CalculateInsertIndex(ItemsControl itemsControl, Avalonia.Point point) {
        int itemCount = itemsControl.ItemCount;

        for (int i = 0; i < itemCount; i++) {
            var container = itemsControl.ContainerFromIndex(i);
            if (container is Control c) {
                var bounds = c.Bounds;
                if (point.Y < bounds.Top + bounds.Height / 2)
                    return i;
            }
        }
        return itemCount; // insert at end
    }
}
