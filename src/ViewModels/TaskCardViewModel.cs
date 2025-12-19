/* Wraps a taskcard model with a taskcard viewmodel to mainly edit the fields 
 * and state of the taskcard when changed in the UI
 */
using System.ComponentModel;
using System.Windows.Input;
using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using App.Models;
using App.Helpers;
using App.Views;

namespace App.ViewModels {

    public class TaskCardViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event Action? AnUpdateHasOccured;
        public ICommand OpenEditorCommand { get; }
        public TaskCard TaskCardModel;
        // Must store parentColumn so a card can remove itself from the list
        public ColumnViewModel ParentColumn { get; set; }

        /* Updates the value of the task name dynamically if edited in UI*/
        public string TaskName {
            get => TaskCardModel.TaskName;
            set {
                if (TaskCardModel.TaskName != value) {
                    TaskCardModel.TaskName = value; 
                    OnPropertyChanged(nameof(TaskName)); 
                    AnUpdateHasOccured?.Invoke();
                }
            }
        }

        public string TaskDescription { 
            get => TaskCardModel.TaskDescription;
            set { 
                if (TaskCardModel.TaskDescription != value) {
                    TaskCardModel.TaskDescription = value;
                    OnPropertyChanged(nameof(TaskDescription)); 
                    AnUpdateHasOccured?.Invoke();
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
                    AnUpdateHasOccured?.Invoke();
                }
            }
        }

        public TaskCardViewModel(TaskCard taskCard, ColumnViewModel parent) {
            TaskCardModel = taskCard;
            ParentColumn = parent;
            OpenEditorCommand = new RelayCommand(OpenEditor);
        } 

        /* Opens the editor window for a taskcard so that the taskcard fields 
         * can be changed. Refer to TaskEditorViewModel and view for more info
         */
        private async void OpenEditor() { 
            TaskEditorViewModel tevm = new TaskEditorViewModel(this);
            TaskEditorView editor = new TaskEditorView { 
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
