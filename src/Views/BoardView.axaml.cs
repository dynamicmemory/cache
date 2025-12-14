using Avalonia.Controls;
using Avalonia.Input;
using App.ViewModels;

namespace App.Views;

public partial class BoardView : UserControl {

    public BoardView() {
        InitializeComponent();
        // DataContext = new BoardViewModel();
    }



private void Columns_DragOver(object? sender, DragEventArgs e)
    {
        e.DragEffects = DragDropEffects.Move;
    }

private void Columns_Drop(object? sender, DragEventArgs e)
{
    if (sender is ItemsControl itemsControl && itemsControl.DataContext is BoardViewModel boardVm)
    {
        if (e.Data.Contains("column") && e.Data.Get("column") is ColumnViewModel draggedColumn)
        {
            var position = e.GetPosition(itemsControl);
            int insertIndex = CalculateColumnInsertIndex(itemsControl, position);

            boardVm.MoveColumn(draggedColumn, insertIndex);
        }
    }
}

private int CalculateColumnInsertIndex(ItemsControl itemsControl, Avalonia.Point point)
{
    int itemCount = itemsControl.ItemCount;

    for (int i = 0; i < itemCount; i++)
    {
        var container = itemsControl.ItemContainerGenerator.ContainerFromIndex(i);
        if (container is Control c)
        {
            var bounds = c.Bounds;
            if (point.X < bounds.Left + bounds.Width / 2)
                return i;
        }
    }

    return itemCount; // drop at end
}



}
