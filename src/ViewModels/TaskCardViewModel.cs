using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using System.ComponentModel;
using System.Windows.Input;
using App.Models;
using App.Helpers;
using App.Views;

namespace App.ViewModels {

    public class TaskCardViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public TaskCard TaskCardModel;

        /* Updates the value of the task name dynamically if edited in UI*/
        public string TaskName {
            get => TaskCardModel.TaskName;
            set {
                if (TaskCardModel.TaskName != value) {
                    TaskCardModel.TaskName = value; 
                    OnPropertyChanged(nameof(TaskName)); 
                }
            }
        }

        // private string _taskDescription;
        public string TaskDescription { 
            get => TaskCardModel.TaskDescription;
            set { 
                if (TaskCardModel.TaskDescription != value) {
                    TaskCardModel.TaskDescription = value;
                    OnPropertyChanged(nameof(TaskDescription)); 
                }
            }
        }
        
        /* Updates the tasks colour dynamically if edited in UI */
        public string TaskColour {
            get => TaskCardModel.TaskColour;
            set { 
                if (TaskCardModel.TaskColour != value) {
                    TaskCardModel.TaskColour = value; 
                    OnPropertyChanged(nameof(TaskColour)); 
                }
            }
        }

        public ICommand OpenEditorCommand { get; }

        public TaskCardViewModel(TaskCard taskCard) {
            TaskCardModel = taskCard;
            // _taskDescription = TaskCardModel.TaskDescription;
            OpenEditorCommand = new RelayCommand(OpenEditor);
        } 

        private async void OpenEditor() { 
            var tevm = new TaskEditorViewModel(this);
            var editor = new TaskEditorView { 
                DataContext = tevm,
                WindowStartupLocation = WindowStartupLocation.CenterOwner};

            // Makes the editor popup appear in the center of the app Window
            // Ugly solution but possibly an Arch problem i'm having atm.
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow is Window mainWindow) {
                await editor.ShowDialog(mainWindow);

            }
        }
            

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
