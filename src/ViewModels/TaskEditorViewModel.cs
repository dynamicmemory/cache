using System.ComponentModel;
using System.Collections.Generic;

namespace App.ViewModels;

public class TaskEditorViewModel : INotifyPropertyChanged {
    public TaskCardViewModel _original;
    private readonly string _taskName;
    private readonly string _taskDescription;
    private readonly string _taskColour;

    public string TaskName { 
        get => _original.TaskName; 
        set => _original.TaskName = value; }

    public string TaskDescription{ 
        get => _original.TaskDescription; 
        set => _original.TaskDescription= value; }

    public string TaskColour{ 
        get => _original.TaskColour; 
        set => _original.TaskColour = value; }

    public IReadOnlyList<string> AvailableColours { get; } = new[] {
        "Magenta", "Purple", "Bulma", "Green", "Lime", "Blue", "Yellow", 
        "Orange", "Red", "White"
    };

    public TaskEditorViewModel(TaskCardViewModel task) {
        _original = task;
        // Keep a copy of the originals incase of cancelling the edit
        _taskName = task.TaskName;
        _taskDescription = task.TaskDescription;
        _taskColour = task.TaskColour;
    }

    /* Retores the original values on cancel only*/
    public void Cancel() {
        _original.TaskName = _taskName;
        _original.TaskDescription = _taskDescription;
        _original.TaskColour = _taskColour;
    }


    /* Helper function for updating the UI when a class property changes*/
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
