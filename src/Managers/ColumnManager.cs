/* It wont compile using "using App.Board" So I had to use "App.Board.Board"*/
using System.ComponentModel;
using App.Models;
using System.Collections.ObjectModel; 

namespace App.Managers {
    public class ColumnManager : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _colName;
        private bool _isEditing;

        public string ColName {
            get => _colName;
            set {
                if (_colName != value) {
                    _colName = value;
                    OnPropertyChanged(nameof(ColName));
                }
            }
        }

        public bool IsEditing {
            get => _isEditing;
            set {
                if (_isEditing != value) {
                    _isEditing = value;
                    OnPropertyChanged(nameof(IsEditing));
                    OnPropertyChanged(nameof(IsNotEditing));
                }
            }
        }

        // public TaskManager Manager { get; }
        public ObservableCollection<TaskCard> TaskList { get; }
        public int NumberOfTasks { get; set; }
        public App.Board.Board? ParentBoard { get; }
        public bool IsNotEditing => !_isEditing;

        // Controls X button visibility
        public bool Removable => this != ParentBoard?.FirstColumn;
        public bool NameEditable => this != ParentBoard?.FirstColumn;

        public ColumnManager(string name, App.Board.Board parent) {
            TaskList = new ObservableCollection<TaskCard>();
            // Manager = new TaskManager();
            _colName = name;
            ParentBoard = parent;
        }

        public void AddNewTask() {
            TaskCard tc = new TaskCard($"New Task Added {NumberOfTasks}");
            TaskList.Add(tc);
            NumberOfTasks++;
        }

        /* For moving a card within its own column*/
        public void MoveTask(TaskCard card, int idx) {
            if (card == null) return;

            var oldidx = TaskList.IndexOf(card);
            if (oldidx == -1) return;

            if (idx < 0) idx = 0;
            if (idx >= TaskList.Count) idx = idx - 1;

            if (oldidx == idx) return;

            TaskList.Move(oldidx, idx);
            
        }
        
        public void RemoveTask(TaskCard card) {
            TaskList.Remove(card);
        }

        public void InsertTask(TaskCard card, int idx) {
            TaskList.Insert(idx, card); 
        }

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
