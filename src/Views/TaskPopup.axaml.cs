using Avalonia.Controls;

using Avalonia.Interactivity;
using App.Models;


namespace App.Views { 

    public partial class TaskPopup : Window {

        private TempTaskCard? _task { get; }

        public TaskPopup() {
            InitializeComponent();
        }

        public TaskPopup(TempTaskCard task) {
            InitializeComponent();
            _task = task;
            DataContext = _task;
        }

        // TODO: Saving like this is gross, fix it
        private void Save_Click(object? sender, RoutedEventArgs e) {
            Close(true);
        }

        private void Cancel_Click(object? sender, RoutedEventArgs e) {
            Close(false);
        }
    }
}
