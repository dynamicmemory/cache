using System.Collections.Generic;
using System.ComponentModel;
using App.Models;

namespace App.ViewModels {

    public class TaskCardViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly TaskCard _tcard;

        /* Updates the value of the task name dynamically if edited in UI*/
        public string TaskName {
            get => _tcard.TaskName;
            set { _tcard.TaskName = value; OnPropertyChanged(nameof(_tcard.TaskName)); }
        }

        // TODO: Make use of task description
        // public string TaskDescription { get; set; }
        
        /* Updates the tasks colour dynamically if edited in UI */
        public string TaskColour {
            get => _tcard.TaskColour;
            set { _tcard.TaskColour = value; OnPropertyChanged(nameof(TaskColour)); }
        }

        // TODO: We should enum this.
        public List<string> AvailableColours { get; } = new List<string>() { 
            "Magenta", "Purple", "Bulma", "Green", "Lime", "Blue",
            "Trunks", "Yellow", "Orange", "Red", "BluPurp", "White", "Transp", 
        };

        public TaskCardViewModel(TaskCard taskCard) {
            _tcard = taskCard;
        } 

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
