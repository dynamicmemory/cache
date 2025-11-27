/* Models a task card that sits inside a column on the taskboard*/
using System.ComponentModel;
using Avalonia.Media;

namespace App.Models {

    public class TaskCard : INotifyPropertyChanged {

    // =========================== CLASS PROPERTIES ==========================
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

        public string TaskDescription { get; set; }
        
        /* Updates the tasks colour dynamically if edited in UI */
        // private Color _taskColour = Color.Parse("#FFDEFF");
        private string _taskColour = "#FFDEFF";
        public string TaskColour {
            get => _taskColour;
            set {
                if (_taskColour != value) {
                    _taskColour = value;
                    OnPropertyChanged(nameof(TaskColour));
                    // OnPropertyChanged(nameof(TaskBrush));
                }
            }
        }
        // public IBrush TaskBrush => new SolidColorBrush(TaskColour);

        // public static readonly Color[] AvailableColours = new Color[] {
        //     Color.Parse("#FF0000"),
            // Color.Parse("#FFFFFF"),
            // Color.Parse("#000000")
        // };
        // public Color[] AvailableColoursInstance => AvailableColours;

        public bool TaskState { get; set; }         // May remove
        public string[] AvailableColours { get; } = new string[] { "#FFFFFF", "#FFDEFF", "#FF0000" };

    // ===========================                  ==========================
        /* Constructor */
        public TaskCard(string name, string desc="") {
            _taskName = name;
            TaskDescription = desc;
            TaskState = false;
            // TaskColour = colour;
        }

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string pName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(pName));
        }

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
