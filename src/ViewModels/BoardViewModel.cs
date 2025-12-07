// TODO: Switch from tempBoard to Board once we fully swap the system over.
// TODO: Figure out our json persistence when time comes.
using System.Collections.ObjectModel;
using System.Linq;
using App.Models;

namespace App.ViewModels {

    public class BoardViewModel {
        public string Name { get; set; }
        public ObservableCollection<ColumnViewModel> Columns { get; }  

        public BoardViewModel(TempBoard tempBoard) {
            Name = tempBoard.BoardName;

            Columns = new ObservableCollection<ColumnViewModel>(
                    tempBoard.Columns.Select(c => new ColumnViewModel(c))
                    );
        }

        /* Add a new column to the ObservableCollection of columns*/
        public void AddColumn() {
            Column column = new Column();
            Columns.Add(new ColumnViewModel(column));
            // JsonDB.SaveBoard()??
        }

        /* Removes a column to the ObservableCollection of columns*/
        public void RemoveColumn(ColumnViewModel column) {
            Columns.Remove(column);
            // JsonDB.SaveBoard()??
        }

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
    }
}
