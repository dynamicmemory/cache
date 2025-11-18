/* Models a task card that sits inside a column on the taskboard*/

namespace App.Models {

    public class TaskCard {

        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool TaskState { get; set; }         // May remove
        public string TaskColour { get; set; }      // Change to enum eventually

        public TaskCard(string name, string desc="", string colour="White") {
            TaskName = name;
            TaskDescription = desc;
            TaskState = false;
            TaskColour = colour;
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
