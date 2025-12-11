/* It wont compile using "using App.Board" So I had to use "App.Board.Board"*/
using System.ComponentModel;
using System.Text.Json.Serialization;
using App.Models;
using System.Collections.ObjectModel; 

namespace App.Managers {
    public class ColumnManager : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        [JsonIgnore]
        public App.TempBoard.TempBoard ParentBoard { get; set; }
        public ObservableCollection<TempTaskCard> TaskList { get; set; }

        // Controls X button visibility
        public bool Removable => this != ParentBoard?.FirstColumn;
        public bool NameEditable => this != ParentBoard?.FirstColumn;

        // Updates name when change in the UI
        private string _colName;
        public string ColName { get => _colName;
            set { if (_colName != value) {
                _colName = value;
                OnPropertyChanged(nameof(ColName));
                }
            }
        }

        // Fields so setting a column to "editing" or not
        private bool _isEditing;
        public bool IsNotEditing => !_isEditing;
        public bool IsEditing { get => _isEditing;
            set {
                if (_isEditing != value) {
                    _isEditing = value;
                    OnPropertyChanged(nameof(IsEditing));
                    OnPropertyChanged(nameof(IsNotEditing));
                }
            }
        }

        public ColumnManager() {}

        public ColumnManager(App.TempBoard.TempBoard parent) {
            ParentBoard = parent;
            TaskList = new ObservableCollection<TempTaskCard>();
            _colName = "New Column";
        }

        public void AddNewTask() {
            TaskList.Add(new TempTaskCard()); 
            TempJsonDB.SaveBoard(ParentBoard);
        }

        /* For moving a card within its own column*/
        public void MoveTask(TempTaskCard card, int idx) {
            if (card == null) return;

            int oldidx = TaskList.IndexOf(card);
            if (oldidx == -1) return;

            if (idx < 0) idx = 0;
            if (idx > TaskList.Count) idx = TaskList.Count;

            if (oldidx == idx || oldidx + 1 == idx) return;

            
            TaskList.Move(oldidx, System.Math.Min(idx, TaskList.Count-1));
            TempJsonDB.SaveBoard(ParentBoard);
        }
        
        /* Removes a given taskcard from the columnlist*/
        public void RemoveTask(TempTaskCard card) {
            TaskList.Remove(card);
            TempJsonDB.SaveBoard(ParentBoard);
        }

        /* Inserts a given taskcard at the given index*/
        public void InsertTask(TempTaskCard card, int idx) {
            TaskList.Insert(idx, card); 
            TempJsonDB.SaveBoard(ParentBoard);
        }

        /* Helper function for updating the UI when a class property changes*/
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
