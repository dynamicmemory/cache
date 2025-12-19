/* Wraps a board in a viewmodel and converts its column list into an observable 
 * list so that realtime changes are displayed on the UI. Acts as the main go 
 * between for the board model and the board view 
 */
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

            // Needed for when we load from json, must add all listeners again
            Columns = new(BoardModel.Columns.Select(c => CreateCVM(c)));

            AddColumnCommand = new RelayCommand(AddColumn);
        }

        /* Creates and sets up a new column added all necessary listeners to 
         * the columns and their tasks 
         */
        public ColumnViewModel CreateCVM(Column col) {
            ColumnViewModel cvm = new(col);
            cvm.AnUpdateHasOccured += OnChildChanged;
            cvm.RemoveReq += OnRemoveReq; 

            // Attaches the update to each task so that taskcard changes bubble 
            // upto the board for saving after any edits.
            foreach (TaskCardViewModel t in cvm.Tasks) {
                t.AnUpdateHasOccured += cvm.OnChildChanged;
            }
            return cvm;
        }

        /* Helper function for a column to remove itself from the columns list
         * Works as a hook that is called when a column calls remove on itself
         */
        private void OnRemoveReq(ColumnViewModel cvm) {
            // Standard redundant null check
            if (Columns.Contains(cvm)) {
                RemoveColumn(cvm);
                BoardModel.Columns.Remove(cvm.ColumnModel);
            }
        }

        /* Triggered if a column or task updates itself, bubbles upto board to 
         * save. 
         */
        private void OnChildChanged() {
            JsonDB.SaveBoard(this);
        }

        /* Add a new column to the ObservableCollection of columns */
        public void AddColumn() {
            Column column = new Column();
            BoardModel.Columns.Add(column);

            Columns.Add(CreateCVM(column));      // Sent to get attachments 
            JsonDB.SaveBoard(this);
        }

        /* Removes a column to the ObservableCollection of columns */
        public void RemoveColumn(ColumnViewModel column) {
            Columns.Remove(column);
            BoardModel.Columns.Remove(column.ColumnModel); 
            JsonDB.SaveBoard(this);
        }

        /* Moves a column to the dropped location of the column*/
        public void MoveColumn(ColumnViewModel column, int idx) {
            // Get the index that the column currently is 
            int oldIdx = Columns.IndexOf(column);

            // clamp idx to stay inbounds of the column list
            if (idx >= Columns.Count) idx = Columns.Count -1;
            // If dropping a column in the same spot, skip
            if (oldIdx == idx) return;

            Columns.Move(oldIdx, idx);
            
            // Adds the column order change to the underlying boardmodel
            BoardModel.Columns.Clear();
            foreach(ColumnViewModel columnVm in Columns) {
                BoardModel.Columns.Add(columnVm.ColumnModel);
            }
            JsonDB.SaveBoard(this);
        }
    }
}
