/* It wont compile using "using App.Board" So I had to use "App.Board.Board"*/
using System.ComponentModel;
using System.Text.Json.Serialization;
using App.Models;
using System.Collections.ObjectModel; 

namespace App.Managers {
    public class ColumnManager : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        [JsonIgnore]
        public App.Board.Board ParentBoard { get; set; }
        public ObservableCollection<TaskCard> TaskList { get; set; }

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

        public ColumnManager(App.Board.Board parent) {
            ParentBoard = parent;
            TaskList = new ObservableCollection<TaskCard>();
            _colName = "New Column";
        }

        public void AddNewTask() {
            TaskList.Add(new TaskCard($"New Task")); 
            JsonDB.SaveBoard(ParentBoard);
        }

        /* For moving a card within its own column*/
        public void MoveTask(TaskCard card, int idx) {
            if (card == null) return;

            int oldidx = TaskList.IndexOf(card);
            if (oldidx == -1) return;

            if (idx < 0) idx = 0;
            if (idx > TaskList.Count) idx = TaskList.Count;

            if (oldidx == idx || oldidx + 1 == idx) return;

            
            TaskList.Move(oldidx, System.Math.Min(idx, TaskList.Count-1));
            JsonDB.SaveBoard(ParentBoard);
        }
        
        /* Removes a given taskcard from the columnlist*/
        public void RemoveTask(TaskCard card) {
            TaskList.Remove(card);
            JsonDB.SaveBoard(ParentBoard);
        }

        /* Inserts a given taskcard at the given index*/
        public void InsertTask(TaskCard card, int idx) {
            TaskList.Insert(idx, card); 
            JsonDB.SaveBoard(ParentBoard);
        }

        /* Helper function for updating the UI when a class property changes*/
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
