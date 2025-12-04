using System.Collections.ObjectModel;
using App.Managers;

namespace App.Board {

    public class Board {
        public ObservableCollection<ColumnManager> ColumnList { get; set; } 
        public ColumnManager FirstColumn { get; set; }

        public Board() {
            // Needed to control column ones permissions, must be set on first creation
            ColumnList = new ObservableCollection<ColumnManager>();
            AddColumn("TASK");
            FirstColumn = ColumnList[0];
        }

        // TODO: Remove ability to pass a name into addcolumn, except for col 1
        /* Helper function for adding a new column to the ColumnList*/
        public void AddColumn(string columnName="New Column") {
            ColumnList.Add(new ColumnManager(columnName, this));            
            JsonDB.SaveBoard(this);
        }

        /* Helper function for removing a column as long as it's not the first
         * column of ColumnList*/ 
        public void RemoveColumn(ColumnManager column) {
            if (column == FirstColumn) return;
            ColumnList.Remove(column);
            JsonDB.SaveBoard(this);
        }

        // TODO: Come back to this when i add column switching
        /* Helper for moving the indexes of columns around to match the visual 
         * changes on the frontend*/
        public void MoveColumn(ColumnManager column, int idx) {
            if (column == null) return;

            int oldidx = ColumnList.IndexOf(column);
            if (oldidx == -1) return;

            if (idx < 0) idx = 0;
            if (idx >= ColumnList.Count) idx = ColumnList.Count - 1;

            if (oldidx == idx) return;

            ColumnList.Move(oldidx, idx);
            JsonDB.SaveBoard(this);
        }
    }
}
