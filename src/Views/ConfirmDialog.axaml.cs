/* Creates a reusable dialog box for the UI*/
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace App.Views;

public partial class ConfirmDialog : Window {

    public string? Message { get; }

    public ConfirmDialog() {
        InitializeComponent();
    }

    public ConfirmDialog(string message) {
        InitializeComponent();
        Message = message;
        DataContext = this;
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e) {
        Close(false);
    }

    private void Ok_Click(object? sender, RoutedEventArgs e) {
        Close(true);
    }
}
