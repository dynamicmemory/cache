/* It wont compile using "using App.Board" So I had to use "App.Board.Board"*/

namespace App.Managers {

    public class ColumnManager {

        public TaskManager Manager { get; set; }
        public string ColName { get; set; }
        public App.Board.Board? Parent { get; set; }

        // Specifically used for remove button visibility for columns
        public bool Removable => this != Parent?.FirstColumn;

        public ColumnManager(string name, App.Board.Board parent) {
            Manager = new TaskManager();
            ColName = name;
            Parent = parent;
        }
    }
}
