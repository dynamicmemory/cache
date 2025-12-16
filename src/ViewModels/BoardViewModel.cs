using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using App.Models;
using App.Helpers;

namespace App.ViewModels {

    public class BoardViewModel {
        public Board BoardModel { get; }
        public string Name { get; set; }
        public ObservableCollection<ColumnViewModel> Columns { get; }  

        public ICommand AddColumnCommand { get; }

        public BoardViewModel(Board board) {
            BoardModel = board;
            Name = BoardModel.BoardName;
            Columns = new(BoardModel.Columns.Select(c => CreateCVM(c)));

            AddColumnCommand = new RelayCommand(AddColumn);
        }

        /* Creates and sets up a new */
        public ColumnViewModel CreateCVM(Column col) {
            ColumnViewModel cvm = new(col);
            cvm.AnUpdateHasOccured += OnChildChanged;
            cvm.RemoveReq += OnRemoveReq; 

            foreach (var t in cvm.Tasks) {
                t.AnUpdateHasOccured += cvm.OnChildChanged;
            }

            return cvm;
        }

        /* Helper function for a column to remove itself from the columns list
         * Works as a hook that is called when a column calls remove on itself*/
        private void OnRemoveReq(ColumnViewModel cvm) {
            // Standard redundant null check
            if (Columns.Contains(cvm)) {
                RemoveColumn(cvm);
                BoardModel.Columns.Remove(cvm.ColumnModel);
            }
        }

        /* Triggered if a column or task updates itself, bubbles upto board to 
         * save. */
        private void OnChildChanged() {
            JsonDB.SaveBoard(this);
        }

        /* Add a new column to the ObservableCollection of columns*/
        public void AddColumn() {
            Column column = new Column();
            BoardModel.Columns.Add(column);

            Columns.Add(CreateCVM(column));  
            JsonDB.SaveBoard(this);
        }

        /* Removes a column to the ObservableCollection of columns*/
        public void RemoveColumn(ColumnViewModel column) {
            Columns.Remove(column);
            BoardModel.Columns.Remove(column.ColumnModel); 
            JsonDB.SaveBoard(this);
        }

        // TODO: Add underlying BoardModel updating on dragging
        /* Moves a column to the dropped location of the column*/
        public void MoveColumn(ColumnViewModel column, int idx) {
            int currentIdx = Columns.IndexOf(column);
            if (currentIdx == -1) return;

            // clamp
            idx = System.Math.Max(0, System.Math.Min(idx, Columns.Count - 1));

            if (currentIdx == idx || currentIdx + 1 == idx) return;

            Columns.Move(currentIdx, idx);
            // TODO: Update the model with helper function that swaps list order once column dragging works
            // BoardModel.Columns.Move(currentIdx, idx); // keep model in sync if needed
            JsonDB.SaveBoard(this);
        }
    }
}
