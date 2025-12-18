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
        public event Action<ColumnViewModel>? RemoveReq;
        public event Action? AnUpdateHasOccured;
        public ICommand StartEditingCommand { get; }
        public ICommand StopEditingCommand { get; }
        public ICommand AddTaskCardCommand { get; }
        public ICommand CommitColumnNameCommand { get; }
        public ICommand RemoveColumnCommand { get; }
        private string _columnName;
        private bool _isEditing; 
        public bool IsNotEditing => !IsEditing;

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

        public string ColumnName { 
            get => _columnName; 
            set { 
                if (_columnName != value) {
                    _columnName = value;
                    OnPropertyChanged(nameof(ColumnName));
                }
            }
        }

        public ColumnViewModel(Column column) {
            ColumnModel = column;
            _columnName = column.ColumnName;

            Tasks = new(column.Tasks.Select(t => new TaskCardViewModel(t, this)));

            StartEditingCommand = new RelayCommand(() => IsEditing = true);
            StopEditingCommand = new RelayCommand(() => IsEditing = false);
            AddTaskCardCommand = new RelayCommand(AddTaskCard);
            CommitColumnNameCommand = new RelayCommand(() => {
                    ColumnModel.ColumnName = ColumnName;
                    AnUpdateHasOccured?.Invoke();
                    IsEditing = false;
                    });

            // Adds a listener to the board to delete the column on button press
            RemoveColumnCommand = new RelayCommand(() => RemoveReq?.Invoke(this));
        }

        public void AddTaskCard() {
            TaskCard taskCard = new TaskCard();
            ColumnModel.Tasks.Add(taskCard);

            var tvm = new TaskCardViewModel(taskCard, this);
            tvm.AnUpdateHasOccured += OnChildChanged;

            Tasks.Add(tvm); 

            // Alert the board a new task was created.
            AnUpdateHasOccured?.Invoke();
        }

        /* Alerts the column to any changes to a taskcard, this bubbles up to 
         * the board for saving */
        public void OnChildChanged() {
            AnUpdateHasOccured?.Invoke();
        }

        /* For moving a card within its own column*/
        public void MoveTask(TaskCardViewModel card, int idx) {
            // Card doesnt exist?
            if (card == null) return;
            int oldidx = Tasks.IndexOf(card);
            if (oldidx == idx) return;        // Ignore a drop in the same spot

            // If card dropped at bottom of the column
            if (idx >= Tasks.Count) 
                idx = Tasks.Count-1;

            Tasks.Move(oldidx, idx);

            // Adds the taskcard to the new index for the model and saves
            TaskCard Temp = ColumnModel.Tasks[oldidx];
            ColumnModel.Tasks.RemoveAt(oldidx);
            ColumnModel.Tasks.Insert(idx, Temp);
            AnUpdateHasOccured?.Invoke();
        }

        /* Removes a given taskcard from the columnlist*/
        public void RemoveTask(TaskCardViewModel card) {
            Tasks.Remove(card);
            ColumnModel.Tasks.Remove(card.TaskCardModel);

            AnUpdateHasOccured?.Invoke();
        }

        /* Inserts a given taskcard at the given index*/
        public void InsertTask(TaskCardViewModel card, int idx) {
            Tasks.Insert(idx, card); 
            ColumnModel.Tasks.Insert(idx, card.TaskCardModel);

            AnUpdateHasOccured?.Invoke();
        }

        /* Helper function for updating the UI when a class property changes*/
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
