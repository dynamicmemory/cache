using System.ComponentModel;
using System.Collections.Generic;
using App.Models;

namespace App.ViewModels;

public class TaskEditorViewModel : INotifyPropertyChanged {
    // We dont want to edit the original until we click save
    private readonly TaskCard _original;

    private string _taskName;
    public string TaskName { get => _taskName;
        set {
            if (_taskName != value) {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
    }

    private string _taskDescription;
    public string TaskDescription
    {
        get => _taskDescription;
        set
        {
            if (_taskDescription != value)
            {
                _taskDescription = value;
                OnPropertyChanged(nameof(TaskDescription));
            }
        }
    }

    private string _taskColour;
    public string TaskColour
    {
        get => _taskColour;
        set
        {
            if (_taskColour != value)
            {
                _taskColour = value;
                OnPropertyChanged(nameof(TaskColour));
            }
        }
    }

    public IReadOnlyList<string> AvailableColours { get; } = new[] {
        "Magenta", "Purple", "Bulma", "Green", "Lime", "Blue", "Yellow", 
        "Orange", "Red", "White"
    };

    public TaskEditorViewModel(TaskCard task) {
        _original = task;
        _taskName = task.TaskName;
        _taskDescription = task.TaskDescription;
        _taskColour = task.TaskColour;

        // Makes clones of all the properties
        TaskName = task.TaskName;
        TaskDescription = task.TaskDescription;
        TaskColour = task.TaskColour;
    }

    /* Overwrite the original tasks features on save only*/
    public void Save() {
        _original.TaskName = TaskName;
        _original.TaskDescription = TaskDescription;
        _original.TaskColour = TaskColour;

        // OnPropertyChanged(nameof(TaskName));
        // OnPropertyChanged(nameof(TaskDescription));
        // OnPropertyChanged(nameof(TaskColour));
    }

    /* Helper function for updating the UI when a class property changes*/
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
