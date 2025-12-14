/* Models a task card that sits inside a column on the taskboard*/
namespace App.Models {

    public class TaskCard {

        public string TaskName { get; set; }
        public string TaskColour { get; set; }
        public string TaskDescription { get; set; }

        /* Constructor */
        public TaskCard() {
            TaskName = "Task Name";
            TaskDescription = "A new task";
            TaskColour = "White";
        } 
    }
}


