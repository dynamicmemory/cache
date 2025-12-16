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
