/* Initializes an observable list for the UI to bind to and check for updates.
 * Holds a collected list of tasks for a singular column. */

using System.Collections.ObjectModel;
using App.Models;

namespace App.Managers {
    
    public class TaskManager {

        public ObservableCollection<TaskCard> Tasks { get; }
        public int NumberOfTasks { get; set; }

        public TaskManager() {
            Tasks = new ObservableCollection<TaskCard>();
        }

        public void AddNewTask() {
            TaskCard tc = new TaskCard($"New Task Added {NumberOfTasks}");
            Tasks.Add(tc);
            NumberOfTasks++;
        }
    }
}
