using System.Collections.ObjectModel;
using App.Managers;

namespace App.Board {

    public class Board {

        public ObservableCollection<ColumnManager> ColumnList { get; set; }
        public ColumnManager FirstColumn { get; set; }
        public int NumberOfColumns { get; set; }

        public Board() {
            ColumnList = new ObservableCollection<ColumnManager>();
            AddColumn("Tasks");
            FirstColumn = ColumnList[0];
        }

        // TODO: Change the "" and null check on name with correct handling
        /* Help function for adding a new column to the UI*/
        public void AddColumn(string colName="New Column") {
            if (string.IsNullOrEmpty(colName)) colName = "New Column";
            var col = new ColumnManager(colName, this); 
            ColumnList.Add(col);            
            NumberOfColumns++;
        }

        /* Removes a column at a given index*/ 
        public void RemoveColumn(ColumnManager col) {
            if (col == FirstColumn) return;

            ColumnList.Remove(col);
            NumberOfColumns--;
        }
    }
}
