/* Models the base board that all columns sit inside of */
using System.Collections.Generic;

namespace App.Models {

    public class Board {
        public List<Column> Columns { get; set; } = new List<Column>();
        public string BoardName { get; set; } = "Cache"; // Mostly irrelevant

        public Board() {}
    }
}
