using System.Collections.ObjectModel;
using App.Managers;

namespace App.Board {

    public class Board {

        public ObservableCollection<ColumnManager> ColumnList { get; set; }

        public Board() {
            ColumnList = new ObservableCollection<ColumnManager>();
            ColumnManager col1 = new ColumnManager("Tasks");
            ColumnManager col2 = new ColumnManager("Doing");
            ColumnManager col3 = new ColumnManager("Finished");
            ColumnList.Add(col1);
            ColumnList.Add(col2);
            ColumnList.Add(col3);

        }
    }
}
