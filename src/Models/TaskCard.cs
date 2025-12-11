/* Models a task card that sits inside a column on the taskboard*/
namespace App.Models {

    public class TaskCard {

        public string TaskName { get; set; }
        public string TaskColour { get; set; }

        /* Constructor */
        public TaskCard() {
            TaskName = "New Task";
            TaskColour = "yellow";
        } 

    }
}


