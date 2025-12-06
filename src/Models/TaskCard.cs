/* Models a task card that sits inside a column on the taskboard*/
using System.ComponentModel;

namespace App.Models {

    public class TaskCard : INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        /* Updates the value of the task name dynamically if edited in UI*/
        private string _taskName;
        public string TaskName {
            get => _taskName;
            set {
                if (_taskName != value) {
                    _taskName = value;
                    OnPropertyChanged(nameof(TaskName));
                }
            }
        }

        // TODO: Make use of task description
        // public string TaskDescription { get; set; }
        
        /* Updates the tasks colour dynamically if edited in UI */
        private string _taskColour = "White";
        public string TaskColour {
            get => _taskColour;
            set {
                if (_taskColour != value) {
                    _taskColour = value;
                    OnPropertyChanged(nameof(TaskColour));
                }
            }
        }

        public string[] AvailableColours { get; } = new string[] { 
            "Magenta", "Purple", "Bulma", "Green", "Lime", "Blue",
            "Trunks", "Yellow", "Orange", "Red", "BluPurp", "White", "Transp", 
        };

        /* Constructor */
        public TaskCard() {
            _taskName = "New Task";
        } 

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string pName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pName));
        }
    }
}
