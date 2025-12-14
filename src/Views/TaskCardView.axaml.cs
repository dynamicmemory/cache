using Avalonia.Controls;
using Avalonia.VisualTree;
using App.ViewModels;
using Avalonia.Input;

namespace App.Views;

public partial class TaskCardView : UserControl {

    public TaskCardView() {
        InitializeComponent();
    }

    private void EditTask(object sender, PointerPressedEventArgs e) {
        if (DataContext is TaskCardViewModel vm)
            vm.OpenEditorCommand.Execute(null);
    }

private void Task_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
{
    if (DataContext is TaskCardViewModel taskVm)
    {
        // Find the ColumnViewModel via the visual tree
        if (VisualRoot is null) return;

        // The DataContext of the parent ColumnView is your source column
        if (this.FindAncestorOfType<ColumnView>()?.DataContext is ColumnViewModel sourceColumn)
        {
            var dataObject = new DataObject();
            dataObject.Set("task", taskVm);
            dataObject.Set("sourceColumn", sourceColumn); // crucial
            DragDrop.DoDragDrop(e, dataObject, DragDropEffects.Move);
        }
    }
}
}
