using System.Windows;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using App.ViewModels;

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


private void Column_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
{
    if (sender is Border border && border.DataContext is ColumnViewModel columnVm)
    {
        var dataObject = new DataObject();
        dataObject.Set("column", columnVm);
        DragDrop.DoDragDrop(e, dataObject, DragDropEffects.Move);
    }
}








    // TASK DRAGGING

    private void TasksList_DragOver(object? sender, DragEventArgs e)
    {
        e.DragEffects = DragDropEffects.Move;
    }

    private void TasksList_Drop(object? sender, DragEventArgs e)
{
    if (sender is ItemsControl itemsControl && itemsControl.DataContext is ColumnViewModel targetColumn)
    {
        if (e.Data.Contains("task") && e.Data.Get("task") is TaskCardViewModel taskVm &&
            e.Data.Contains("sourceColumn") && e.Data.Get("sourceColumn") is ColumnViewModel sourceColumn)
        {
            var position = e.GetPosition(itemsControl);
            int insertIndex = CalculateInsertIndex(itemsControl, position);

            if (sourceColumn == targetColumn)
            {
                // Single-column reordering
                sourceColumn.MoveTask(taskVm, insertIndex);
            }
            else
            {
                // Inter-column move
                sourceColumn.RemoveTask(taskVm);
                targetColumn.InsertTask(taskVm, insertIndex);
            }
        }
    }
}
    private int CalculateInsertIndex(ItemsControl itemsControl, Avalonia.Point point)
{
    int itemCount = itemsControl.ItemCount;

    for (int i = 0; i < itemCount; i++)
    {
        var container = itemsControl.ItemContainerGenerator.ContainerFromIndex(i);
        if (container is Control c)
        {
            var bounds = c.Bounds;
            if (point.Y < bounds.Top + bounds.Height / 2)
                return i;
        }
    }

    return itemCount; // insert at end
}}
