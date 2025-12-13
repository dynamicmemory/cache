using Avalonia.Controls;
using Avalonia.Interactivity;
using App.ViewModels;

namespace App.Views;

public partial class TaskEditorView : Window {
    
    public TaskEditorView() {
        InitializeComponent();
    }

    private void Save_Click(object? sender, RoutedEventArgs e) {
        if (DataContext is TaskEditorViewModel vm) {
            vm.Save();
        }

        Close();
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e) {
        Close();
    }
}
