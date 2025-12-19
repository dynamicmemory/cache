/* Code behind for the taskcardview, Handles editing taskcards and initiating 
 * the dragging logic 
 */
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

    /* Handles clicks on the taskcard name to open up the editor to make changes 
     * to the card 
     */ 
    private void EditTask(object sender, PointerPressedEventArgs e) {
        // Handle the call for dragging (We want the edit panel not to drag)
        e.Handled=true;
        if (DataContext is TaskCardViewModel vm)
            vm.OpenEditorCommand.Execute(null);
    }

    /* Handles the start of a drag and passes the context off to the columnview 
     * for the dropping portion 
     */
    private void Task_PointerPressed(object? sender, PointerPressedEventArgs e) {
        if (DataContext is not TaskCardViewModel taskVm) return;

        // The DataContext of the parent ColumnView is your source column
        if (this.FindAncestorOfType<ColumnView>()?.DataContext is ColumnViewModel sourceColumn) {
            DragManager.DraggedItem = (taskVm, sourceColumn);
            DragDrop.DoDragDropAsync(e, new DataTransfer(), DragDropEffects.Move);
        }
    }
}
