/* Classes primary function is to control the editing of a task card within the 
 * editors window when a taskcards name is clicked on
 */
using System.ComponentModel;
using System.Collections.Generic;

namespace App.ViewModels;

public class TaskEditorViewModel : INotifyPropertyChanged {
    public TaskCardViewModel _original;
    private readonly string _taskName;
    private readonly string _taskDescription;
    private readonly string _taskColour;

    // These fields auto setting allow for visually seeing the changes in 
    // realtime instead of needing to confirm changes to see how they look.
    public string TaskName { 
        get => _original.TaskName; 
        set => _original.TaskName = value; }

    public string TaskDescription{ 
        get => _original.TaskDescription; 
        set => _original.TaskDescription= value; }

    public string TaskColour{ 
        get => _original.TaskColour; 
        set => _original.TaskColour = value; }

    // TODO: Sort this list out and change the colours in the helper too
    public IReadOnlyList<string> AvailableColours { get; } = new[] {
        "Red",
        "Orange",
        "Yellow",
        "Lime",
        "Bulma",
        "Trunks",
        "Blue",
        "Purple",
        "Magenta",
        "White", 
        "Black",
        "Transparent"
    };

    public TaskEditorViewModel(TaskCardViewModel task) {
        _original = task;

        // Keep a copy of the originals incase of cancelling the edit
        _taskName = task.TaskName;
        _taskDescription = task.TaskDescription;
        _taskColour = task.TaskColour;
    }

    /* Saves the changes made to a taskcard */
    public void Save() {
        // If a user leaves a blank name for the task, this will set it to
        // "empty" as a fallback so that the card is still accessible and editable 
        string newName = _original.TaskName;
        if (string.IsNullOrEmpty(newName)) _original.TaskName = "Empty"; 
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
