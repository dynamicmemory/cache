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
        private string _taskColour = "#FFDEFF";
        public string TaskColour {
            get => _taskColour;
            set {
                if (_taskColour != value) {
                    _taskColour = value;
                    OnPropertyChanged(nameof(TaskColour));
                }
            }
        }

        public bool TaskState { get; set; }         // May remove
        // TODO: Turn this into an enum 
        public string[] AvailableColours { get; } = new string[] { 
            "#FFFFFF", 
            "#FFDEFF", // light pink
            "#9C27F5",// purple
            "#F59C27", // Orange
            "#27F59C",  // bulma
            "#80F527",  // Green
            "#E7F527", // Yellow
            "#27E7F5", // Trunks cyan
            "#F53527", // Red 

        };

    // ===========================                  ==========================
        /* Constructor */
        public TaskCard() {} // Literally just for json to load a card 

        public TaskCard(string name, string desc="") {
            _taskName = name;
            // TaskDescription = desc;
            TaskState = false;
            // TaskColour = colour;
        }

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string pName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pName));
        }

        // TODO: The two below functions arnt being used, find work for them or delete
        /* Updates a given task with any combination of new name, description, 
           and colour*/
        public void Update(string name, string description, string colour) {
        
        }

        /* Updates a given task to completed when moved into the finished column
         */
        public void Complete(TaskCard task) {
            task.TaskState = true;
        }
    }
}
