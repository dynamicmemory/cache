namespace App.Managers {

    public class ColumnManager {

        public TaskManager Manager { get; set; }
        public string ColName { get; set; }

        public ColumnManager(string name) {
            Manager = new TaskManager();
            ColName = name;
        }

        // Set should just do this tho?
        public void UpdateName() {

        }
    }
}
