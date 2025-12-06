// TODO: Column manager has a Parent which was the board, just for reminding myself
// TODO: Maybe change how we do the name, possibly pass in, see how this builds out.
using System.Collections.Generic;

namespace App.Models {

    public class Column {

        public List<TaskCard> Tasks { get; set; } = new List<TaskCard>();
        public string ColumnName { get; set; } 

        public Column() {
            ColumnName = "New Column";
        }
    }
}
