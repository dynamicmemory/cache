/* It wont compile using "using App.Board" So I had to use "App.Board.Board"*/
using System.ComponentModel;

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

        public TaskManager Manager { get; }
        public App.Board.Board? ParentBoard { get; }
        public bool IsNotEditing => !_isEditing;

        // Controls X button visibility
        public bool Removable => this != ParentBoard?.FirstColumn;
        public bool NameEditable => this != ParentBoard?.FirstColumn;

        public ColumnManager(string name, App.Board.Board parent) {
            Manager = new TaskManager();
            _colName = name;
            ParentBoard = parent;
        }

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
