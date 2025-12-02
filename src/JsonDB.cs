using System.Text.Json;
using System.Collections.ObjectModel;
using System.IO;
using App.Board;
using App.Models;
using App.Managers;

public static class JsonDB {
    private const string FilePath = "board.json";

    /* Temporary solution for program persistence, saves a current board*/
    public static void SaveBoard(Board board) {

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(board, options);
        File.WriteAllText(FilePath, json);
    }

    public static Board LoadBoard() {
        
        if (!File.Exists(FilePath)) return new Board();

        var json = File.ReadAllText(FilePath);
        var board = JsonSerializer.Deserialize<Board>(json)!;

        // Converting to observable lists for Avalonia
        foreach (var col in board.ColumnList) {
            col.TaskList = new ObservableCollection<TaskCard>(col.TaskList);
            col.ParentBoard = board;
        }

        board.ColumnList = new ObservableCollection<ColumnManager>(board.ColumnList);
        if (board.ColumnList.Count > 0)
            board.FirstColumn = board.ColumnList[0];
        
        board.NumberOfColumns = board.ColumnList.Count;

        SaveBoard(board);
        return board;
    }
}
