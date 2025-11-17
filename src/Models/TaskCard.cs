/* Models a task card that sits inside a column on the taskboard*/

namespace App.Models {

    public class TaskCard {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }         // May remove
        public string Colour { get; set; }      // Change to enum eventually

        public TaskCard(string name, string desc="", string colour="White") {
            Name = name;
            Description = desc;
            State = false;
            Colour = colour;
        }

        /* Updates a given task with any combination of new name, description, 
           and colour*/
        public void Update(string name, string description, string colour) {
        
        }

        /* Updates a given task to completed when moved into the finished column
         */
        public void Complete(TaskCard task) {
            task.State = true;
        }
    }
}
