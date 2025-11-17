namespace App.Managers {

    public class ColumnManager {

        public TaskManager Manager { get; set; }
        public string Name { get; set; }

        public ColumnManager(string name) {
            Manager = new TaskManager();
            Name = name;
        }

        // Set should just do this tho?
        public void UpdateName() {

        }
    }
}
