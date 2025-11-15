namespace App.Managers {
    public class TaskManager {
        public string CurrentTask { get; set; }

        public TaskManager() {
            CurrentTask = "You got the task!"; 
        }

        public string GetTask() {
            return "You got the task";
        }
    }
}
