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
        private bool _isEditing; 
        public bool IsEditing { 
            get => _isEditing;
            set { if (_isEditing != value) {
                      _isEditing = value;
                      OnPropertyChanged(nameof(IsEditing));
                      OnPropertyChanged(nameof(IsNotEditing));
                  }
            }
        }
        public bool IsNotEditing => !IsEditing;

        private string _columnName;
        public string ColumnName { 
            get => _columnName; 
            set { if (_columnName != value) {
                      _columnName = value;
                      OnPropertyChanged(nameof(ColumnName));
                }
            }
            // Optional to save the board here if i include a board ref in this vm
        }

        public event Action<ColumnViewModel>? RemoveReq;
        public ICommand StartEditingCommand { get; }
        public ICommand StopEditingCommand { get; }
        public ICommand AddTaskCardCommand { get; }
        public ICommand CommitColumnNameCommand { get; }
        public ICommand RemoveColumnCommand { get; }

        public ColumnViewModel(Column column, bool removable) {
            ColumnModel = column;
            _columnName = column.ColumnName;
            Removable = removable;

            Tasks = new(column.Tasks.Select(t => new TaskCardViewModel(t)));

            StartEditingCommand = new RelayCommand(() => IsEditing = true);
            StopEditingCommand = new RelayCommand(() => IsEditing = false);
            AddTaskCardCommand = new RelayCommand(AddTaskCard);
            CommitColumnNameCommand = new RelayCommand(() => {
                    ColumnModel.ColumnName = ColumnName;
                    IsEditing = false;
                    });

            RemoveColumnCommand = new RelayCommand(() => { 
                    RemoveReq?.Invoke(this); }, () => Removable);

            AddTaskCard();
        }

        public void AddTaskCard() {
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
