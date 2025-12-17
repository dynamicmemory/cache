using Avalonia.Controls;
using Avalonia.VisualTree;
using App.ViewModels;
using Avalonia.Input;
using App.Helpers;

namespace App.Views;

public partial class TaskCardView : UserControl {

    public TaskCardView() {
        InitializeComponent();
    }

    private void EditTask(object sender, PointerPressedEventArgs e) {
        // Handle the call for dragging (We want the edit panel not to drag)
        e.Handled=true;
        if (DataContext is TaskCardViewModel vm)
            vm.OpenEditorCommand.Execute(null);
    }

    private void Task_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e) {
        if (DataContext is not TaskCardViewModel taskVm) {
            return;
        }

        // The DataContext of the parent ColumnView is your source column
        if (this.FindAncestorOfType<ColumnView>()?.DataContext is ColumnViewModel sourceColumn) {
            DragManager.DraggedItem = (taskVm, sourceColumn);
            DragDrop.DoDragDropAsync(e, new DataTransfer(), DragDropEffects.Move);

            // var dataObject = new DataObject();
            // dataObject.Set("task", taskVm);
            // dataObject.Set("sourceColumn", sourceColumn); // crucial
            // DragDrop.DoDragDrop(e, dataObject, DragDropEffects.Move);
        }
    }
}
