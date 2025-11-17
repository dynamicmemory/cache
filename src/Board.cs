using System.Collections.ObjectModel;
using App.Managers;

public class Board {

    public ObservableCollection<ColumnManager> ColumnList { get; set; }

    public Board() {
        ColumnList = new ObservableCollection<ColumnManager>();
        ColumnManager Window = new ColumnManager("Tasks");
    }
}
