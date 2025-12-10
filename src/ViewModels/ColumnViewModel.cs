using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System;
using App.Models;
using App.Helpers;

namespace App.ViewModels {

    public class ColumnViewModel : INotifyPropertyChanged {
        public Column ColumnModel { get; }
        public ObservableCollection<TaskCardViewModel> Tasks { get; }
        
        public bool Removable { get; }
        public bool IsEditing { get; set; }
        public bool IsNotEditing => !IsEditing;

        public string ColumnName { 
            get => ColumnModel.ColumnName; 
            set => OnPropertyChanged(nameof(ColumnName));
            // Optional to save the board here if i include a board ref in this vm
        }

        public event Action<ColumnViewModel>? RemoveReq;
        public ICommand StartEditingCommand { get; }
        public ICommand StopEditingCommand { get; }
        public ICommand RemoveColumnCommand { get; }

        public ColumnViewModel(Column column, bool removable) {
            ColumnModel = column;
            ColumnName = column.ColumnName;
            Removable = removable;

            Tasks = new(column.Tasks.Select(t => new TaskCardViewModel(t)));

            StartEditingCommand = new RelayCommand(() => IsEditing = true);
            StopEditingCommand = new RelayCommand(() => IsEditing = false);
            RemoveColumnCommand = new RelayCommand(() => { 
                    RemoveReq?.Invoke(this); }, () => Removable);

            AddNewTask();
        }

        public void AddNewTask() {
            TaskCard taskCard = new TaskCard();
            ColumnModel.Tasks.Add(taskCard);

            Tasks.Add(new TaskCardViewModel(taskCard)); 
            // Optional to save the board here if i include a board ref in this vm
            // JsonDB.SaveBoard(ParentBoard);
        }

        /* For moving a card within its own column*/
        public void MoveTask(TaskCardViewModel card, int idx) {
            if (card == null) return;

            int oldidx = Tasks.IndexOf(card);
            if (oldidx == -1) return;

            if (idx < 0) idx = 0;
            if (idx > Tasks.Count) idx = Tasks.Count;

            if (oldidx == idx || oldidx + 1 == idx) return;
            
            Tasks.Move(oldidx, System.Math.Min(idx, Tasks.Count-1));
            // Optional to save the board here if i include a board ref in this vm
            // JsonDB.SaveBoard(ParentBoard);
        }
        
        /* Removes a given taskcard from the columnlist*/
        public void RemoveTask(TaskCardViewModel card) {
            Tasks.Remove(card);
            ColumnModel.Tasks.Remove(card.TaskCardModel);
            // Optional to save the board here if i include a board ref in this vm
            // JsonDB.SaveBoard(ParentBoard);
        }

        /* Inserts a given taskcard at the given index*/
        public void InsertTask(TaskCardViewModel card, int idx) {
            Tasks.Insert(idx, card); 
            // Optional to save the board here if i include a board ref in this vm
            // JsonDB.SaveBoard(ParentBoard);
        }

        /* Helper function for updating the UI when a class property changes*/
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

// Old version 

/* It wont compile using "using App.Board" So I had to use "App.Board.Board"*/
// using System.ComponentModel;
// using System.Text.Json.Serialization;
// using App.Models;
// using System.Collections.ObjectModel; 
//
// namespace App.Managers {
//     public class ColumnManager : INotifyPropertyChanged {
//         public event PropertyChangedEventHandler? PropertyChanged;
//
//         [JsonIgnore]
//         public App.Board.Board ParentBoard { get; set; }
//         public ObservableCollection<TaskCard> TaskList { get; set; }
//
//         // Controls X button visibility
//         public bool Removable => this != ParentBoard?.FirstColumn;
//         public bool NameEditable => this != ParentBoard?.FirstColumn;
//
//         // Updates name when change in the UI
//         private string _colName;
//         public string ColName { get => _colName;
//             set { if (_colName != value) {
//                 _colName = value;
//                 OnPropertyChanged(nameof(ColName));
//                 }
//             }
//         }
//
//         // Fields so setting a column to "editing" or not
//         private bool _isEditing;
//         public bool IsNotEditing => !_isEditing;
//         public bool IsEditing { get => _isEditing;
//             set {
//                 if (_isEditing != value) {
//                     _isEditing = value;
//                     OnPropertyChanged(nameof(IsEditing));
//                     OnPropertyChanged(nameof(IsNotEditing));
//                 }
//             }
//         }
//
//         public ColumnManager() {}
//
//         public ColumnManager(App.Board.Board parent) {
//             ParentBoard = parent;
//             TaskList = new ObservableCollection<TaskCard>();
//             _colName = "New Column";
//         }
//
//         public void AddNewTask() {
//             TaskList.Add(new TaskCard()); 
//             JsonDB.SaveBoard(ParentBoard);
//         }
//
//         /* For moving a card within its own column*/
//         public void MoveTask(TaskCard card, int idx) {
//             if (card == null) return;
//
//             int oldidx = TaskList.IndexOf(card);
//             if (oldidx == -1) return;
//
//             if (idx < 0) idx = 0;
//             if (idx > TaskList.Count) idx = TaskList.Count;
//
//             if (oldidx == idx || oldidx + 1 == idx) return;
//
//
//             TaskList.Move(oldidx, System.Math.Min(idx, TaskList.Count-1));
//             JsonDB.SaveBoard(ParentBoard);
//         }
//
//         /* Removes a given taskcard from the columnlist*/
//         public void RemoveTask(TaskCard card) {
//             TaskList.Remove(card);
//             JsonDB.SaveBoard(ParentBoard);
//         }
//
//         /* Inserts a given taskcard at the given index*/
//         public void InsertTask(TaskCard card, int idx) {
//             TaskList.Insert(idx, card); 
//             JsonDB.SaveBoard(ParentBoard);
//         }
//
//         /* Helper function for updating the UI when a class property changes*/
//         protected void OnPropertyChanged(string propertyName) {
//             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//         }
//     }
// }
