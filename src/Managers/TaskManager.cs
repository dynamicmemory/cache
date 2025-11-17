/* Initializes an observable list for the UI to bind to and check for updates.
 * Holds a collected list of tasks for a singular column. */

using System.Collections.ObjectModel;
using App.Models;

namespace App.Managers {
    
    public class TaskManager {

        public ObservableCollection<TaskCard> Tasks { get; }

        public TaskManager() {
            Tasks = new ObservableCollection<TaskCard>();
            TaskCard tc = new TaskCard("Test Task", "This is a test task", "blue");
            Tasks.Add(tc);
        }
    }
}
