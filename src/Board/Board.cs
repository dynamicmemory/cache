using System.Collections.ObjectModel;
using App.Managers;

namespace App.Board {

    public class Board {

        public ObservableCollection<ColumnManager> ColumnList { get; set; } = 
            new ObservableCollection<ColumnManager>();
        public ColumnManager FirstColumn { get; set; }
        public int NumberOfColumns { get; set; }

        public Board() {
            // ColumnList = new ObservableCollection<ColumnManager>();
            AddColumn("Tasks");
            FirstColumn = ColumnList[0];
            NumberOfColumns = 0;
            // FirstColumn = null;
        }

        // TODO: Change the "" and null check on name with correct handling
        /* Help function for adding a new column to the UI*/
        public void AddColumn(string colName="New Column") {
            if (string.IsNullOrEmpty(colName)) colName = "New Column";
            var col = new ColumnManager(colName, this); 
            ColumnList.Add(col);            
            NumberOfColumns++;
            JsonDB.SaveBoard(this);
        }

        /* Removes a column at a given index*/ 
        public void RemoveColumn(ColumnManager col) {
            if (col == FirstColumn) return;

            ColumnList.Remove(col);
            NumberOfColumns--;
            JsonDB.SaveBoard(this);
        }

        public void MoveColumn(ColumnManager col, int idx) {
            if (col == null) return;

            var oldidx = ColumnList.IndexOf(col);
            if (oldidx == -1) return;

            if (idx < 0) idx = 0;
            if (idx >= ColumnList.Count) idx = ColumnList.Count - 1;

            if (oldidx == idx) return;

            ColumnList.Move(oldidx, idx);
            JsonDB.SaveBoard(this);
        }
    }
}
