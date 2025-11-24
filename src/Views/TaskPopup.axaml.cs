using Avalonia.Controls;
using Avalonia.Interactivity;
using App.Models;

namespace App.Views { 

    public partial class TaskPopup : Window {

        private TaskCard _task;

        public TaskPopup(TaskCard task) {
            InitializeComponent();
            _task = task;
            DataContext = _task;
        }

        private void Save_Click(object? sender, RoutedEventArgs e) {
            Close(true);
        }

        private void Cancel_Click(object? sender, RoutedEventArgs e) {
            Close(false);
        }
    }
}
