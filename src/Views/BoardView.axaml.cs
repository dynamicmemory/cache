using Avalonia.Controls;
using Avalonia.Input;
using App.ViewModels;
using App.Helpers;

namespace App.Views;

public partial class BoardView : UserControl {

    public BoardView() {
        InitializeComponent();
    }

    private void Columns_DragOver(object? sender, DragEventArgs e) {
        e.DragEffects = DragDropEffects.Move;
    }

    private void Columns_Drop(object? sender, DragEventArgs e) {
        if (DragManager.DraggedItem is not ColumnViewModel draggedColumn) 
            return;

        if (sender is ItemsControl itemsControl && itemsControl.DataContext is BoardViewModel boardVm) {
            int insertIndex = CalculateColumnInsertIndex(itemsControl, e.GetPosition(itemsControl));
            boardVm.MoveColumn(draggedColumn, insertIndex);
        }
        // Reset the dragged item to null for the next operation
        DragManager.DraggedItem = null;
    }

    private int CalculateColumnInsertIndex(ItemsControl itemsControl, Avalonia.Point point) {
        int itemCount = itemsControl.ItemCount;

        for (int i = 0; i < itemCount; i++) {
            var container = itemsControl.ContainerFromIndex(i);
            if (container is Control c) {
                var bounds = c.Bounds;
                if (point.X < bounds.Left + bounds.Width / 2)
                    return i;
            }
        }
        return itemCount; // drop at end
    }

}
