/* Models a task card that sits inside a column on the taskboard*/
using System.ComponentModel;

namespace App.Models {

    public class TaskCard : INotifyPropertyChanged {


        public event PropertyChangedEventHandler? PropertyChanged;
        public string TaskDescription { get; set; }
        public bool TaskState { get; set; }         // May remove
        public string TaskColour { get; set; }      // Change to enum eventually
        private string _taskName;

        /* Updates the value of the task name dynamically if edited in UI*/
        public string TaskName {
            get => _taskName;
            set {
                if (_taskName != value) {
                    _taskName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaskName)));
                }
            }
        }

        /* Constructor */
        public TaskCard(string name, string desc="", string colour="White") {
            _taskName = name;
            TaskDescription = desc;
            TaskState = false;
            TaskColour = colour;
        }

        /* Helper for events - Sets the propertyName with the updated value if 
         * it has dynamically changed at run time*/ 
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
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
