// TODO: Switch from tempBoard to Board once we fully swap the system over.
// TODO: Figure out our json persistence when time comes.
using System.Collections.ObjectModel;
using System.Linq;
using App.Models;

namespace App.ViewModels {

    public class BoardViewModel {
        public Board BoardModel { get; }
        public string Name { get; set; }
        public ObservableCollection<ColumnViewModel> Columns { get; }  

        public BoardViewModel() {
            BoardModel = new Board();
            Name = BoardModel.BoardName;
            // Removable if idx > 0
            Columns = new(BoardModel.Columns.Select((c, idx) => CreateCVM(c, idx)));

            // Possibly load the Json.loadboard() here

            // Currently adding a column for testing and building ui
            AddColumn();
        }

        /* Creates and sets up a new */
        private ColumnViewModel CreateCVM(Column col, int idx) {
            ColumnViewModel cvm = new(col, removable: idx > 0);
            cvm.RemoveReq += OnRemoveReq; 
            return cvm;
        }

        private void OnRemoveReq(ColumnViewModel cvm) {
            // Standard redundant null check
            if (Columns.Contains(cvm)) {
                RemoveColumn(cvm);
                BoardModel.Columns.Remove(cvm.ColumnModel);
            }
        }

        /* Add a new column to the ObservableCollection of columns*/
        public void AddColumn() {
            Column column = new Column();
            BoardModel.Columns.Add(column);

            Columns.Add(CreateCVM(column, 1));     // 1 to indicate deletable column
            // JsonDB.SaveBoard()??
        }

        /* Removes a column to the ObservableCollection of columns*/
        public void RemoveColumn(ColumnViewModel column) {
            Columns.Remove(column);
            BoardModel.Columns.Remove(column.ColumnModel); 
            // JsonDB.SaveBoard()??
        }

        // TODO: Add underlying BoardModel updating on dragging
        /* Moves a column to the dropped location of the column*/
        public void MoveColumn(ColumnViewModel column, int idx) {
            // Maybe should be check in the view??
            if (column == null) return;

            int currentIdx = Columns.IndexOf(column);
            // Maybe impossible, dont know yet??
            if (currentIdx == -1) return;

            // Dragged above first column or below last column in list visually
            if (idx < 0) idx = 0;
            if (idx >= Columns.Count) idx = Columns.Count - 1;
            // JsonDB.SaveBoard()??
        }

        public string printText() {
            return "It worked"; 
        }
    }
}
