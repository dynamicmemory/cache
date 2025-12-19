/* Code behind for the taskcard editor, calls save and cancel from the viewmodel 
 * as well as handles taskcard deletion 
 */ 
using Avalonia.Controls;
using Avalonia.Interactivity;
using App.ViewModels;
using App.Views;

namespace App.Views;

public partial class TaskEditorView : Window {
    
    public TaskEditorView() {
        InitializeComponent();
    }

    /* Handles when the save button is clicked */
    private void Save_Click(object? sender, RoutedEventArgs e) {
        if (DataContext is TaskEditorViewModel vm) vm.Save();
        Close();
    }

    /* Handles when the cancel button is clicked */
    private void Cancel_Click(object? sender, RoutedEventArgs e) {
        if (DataContext is TaskEditorViewModel vm) vm.Cancel();
        
        Close();
    }

    /* Handles when the delete button is clicked */
    private async void Delete_Click(object? sender, RoutedEventArgs e) {
        if (DataContext is TaskEditorViewModel vm ) {
            // Remove the current task
            var popup = new ConfirmDialogView("Are you sure you want to delete this task?");
            bool result = await popup.ShowDialog<bool>(this);

            if (result) {
            vm._original.ParentColumn.RemoveTask(vm._original);
            Close();
            }
        }
    }
}
