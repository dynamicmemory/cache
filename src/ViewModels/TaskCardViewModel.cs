
using System.Collections.Generic;
using System.ComponentModel;
using App.Models;

namespace App.ViewModels {

    public class TaskCardViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        public TaskCard TaskCardModel;

        /* Updates the value of the task name dynamically if edited in UI*/
        public string TaskName {
            get => TaskCardModel.TaskName;
            set { TaskCardModel.TaskName = value; 
                OnPropertyChanged(nameof(TaskCardModel.TaskName)); }
       }

        // TODO: Make use of task description
        // public string TaskDescription { get; set; }
        
        /* Updates the tasks colour dynamically if edited in UI */
        public string TaskColour {
            get => TaskCardModel.TaskColour;
            set { TaskCardModel.TaskColour = value; 
                OnPropertyChanged(nameof(TaskColour)); }
        }

        // TODO: We should enum this.
        public List<string> AvailableColours { get; } = new List<string>() { 
            "Magenta", "Purple", "Bulma", "Green", "Lime", "Blue",
            "Trunks", "Yellow", "Orange", "Red", "BluPurp", "White", "Transp", 
        };

        public TaskCardViewModel(TaskCard taskCard) {
            TaskCardModel = taskCard;
        } 

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string property) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
