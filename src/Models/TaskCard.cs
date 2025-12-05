/* Models a task card that sits inside a column on the taskboard*/
using System.ComponentModel;
// using Avalonia.Media;

namespace App.Models {

    public class TaskCard : INotifyPropertyChanged {

    // =========================== CLASS PROPERTIES ==========================
        public event PropertyChangedEventHandler? PropertyChanged;

        /* Updates the value of the task name dynamically if edited in UI*/
        private string _taskName = "New Task";
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
        // private Color _taskColour = Color.Parse("#FFDEFF");
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
            "Magenta",
            "Purple",
            "Bulma",
            "Green",
            "Lime",
            "Blue",
            "Trunks", 
            "Yellow",
            "Orange",
            "Red",
            "BluPurp",
            "White", 
        };

    // ===========================                  ==========================
        /* Constructor */
        public TaskCard() {} // Literally just for json to load a card 

        public TaskCard(string name, string desc="") {
            _taskName = name;
        }

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string pName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pName));
        }
    }
}
