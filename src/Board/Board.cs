using System.Collections.ObjectModel;
using App.Managers;

namespace App.Board {

    public class Board {

        public ObservableCollection<ColumnManager> ColumnList { get; set; }
        public ColumnManager FirstColumn { get; set; }
        public int NumberOfColumns { get; set; }

        public Board() {
            ColumnList = new ObservableCollection<ColumnManager>();
            AddTaskColumn();
            FirstColumn = ColumnList[0];
        }

        /* Adds the default Task column to the UI*/
        public void AddTaskColumn() {
            ColumnManager col = new ColumnManager("Tasks");
            ColumnList.Add(col);
            NumberOfColumns++;
        }

        /* Help function for adding a new column to the UI*/
        public void AddColumn(string colName) {
            ColumnManager col = new ColumnManager("New Column");
            ColumnList.Add(col);            
            NumberOfColumns++;
        }
    }
}
