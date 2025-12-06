/* Creates a reusable dialog box for the UI*/
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace App.Views;

public partial class ConfirmDialogView : Window {

    public string? Message { get; }

    public ConfirmDialogView() {
        InitializeComponent();
    }

    public ConfirmDialogView(string message) {
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
