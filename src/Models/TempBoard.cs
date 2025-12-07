using System.Collections.Generic;

namespace App.Models {

    public class TempBoard {

        public List<Column> Columns { get; set; } = new List<Column>();
        public string BoardName { get; set; } = "Cache";

        public TempBoard() {

        }
    }
}
