/* Code behind for the boardview, mostly deals with column dragging and dropping
 */
using Avalonia.Controls;
using Avalonia.Input;
using App.ViewModels;
using App.Helpers;

namespace App.Views;

public partial class BoardView : UserControl {

    public BoardView() {
        InitializeComponent();
    }

    /* Animation for column dragging (Yet to be implemented) */
    private void Columns_DragOver(object? sender, DragEventArgs e) {
        e.DragEffects = DragDropEffects.Move;
    }

    /* Handles where a column is dropped on the board, sends the UI information 
     * to the boardviewmodel for action 
     */ 
    private void Columns_Drop(object? sender, DragEventArgs e) {
        if (DragManager.DraggedItem is not ColumnViewModel draggedColumn) 
            return;

        if (sender is ItemsControl itemsControl && itemsControl.DataContext is BoardViewModel boardVm) {
            // Gets the index of the column that was moved and updates the under
            // lying board model and viewmodel to match.
            int insertIndex = CalculateColumnInsertIndex(itemsControl, e.GetPosition(itemsControl));
            boardVm.MoveColumn(draggedColumn, insertIndex);
        }
        // Reset the dragged item to null for the next peration
        DragManager.DraggedItem = null;
    }

    /* Calculates the index of the object that was dragged and dropped */
    private int CalculateColumnInsertIndex(ItemsControl itemsControl, Avalonia.Point point) {
        int itemCount = itemsControl.ItemCount;

        for (int i = 0; i < itemCount; i++) {
            var container = itemsControl.ContainerFromIndex(i);
            if (container is Control c) {
                var bounds = c.Bounds;
                if (point.X < bounds.Left + bounds.Width)
                    return i;
            }
        }
        return itemCount; // drop at end
    }

}
