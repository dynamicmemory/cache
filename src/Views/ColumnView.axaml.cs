using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using App.ViewModels;

namespace App.Views;

public partial class ColumnView : UserControl {

    public ColumnView() {
        InitializeComponent();
    }

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
}
