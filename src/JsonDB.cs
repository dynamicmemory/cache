using System;
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

    /* Loads state from json file or creates a new board to use if no json is 
     * found*/
    public static Board LoadBoard() {
        if (!File.Exists(FilePath)) 
            return new Board();
        
        Board board;
        try {
        string json = File.ReadAllText(FilePath);
        board = JsonSerializer.Deserialize<Board>(json)!;
        board.ColumnList = new ObservableCollection<ColumnManager>(board.ColumnList);
        } catch (JsonException e) {
            Console.WriteLine(e.Message);
            return new Board();
        }

        // You must set the FirstColumn or else Null explosion inside the json
        if (board.ColumnList.Count > 0)
            board.FirstColumn = board.ColumnList[0];

        // Converting to observable lists for Avalonia
        foreach (var col in board.ColumnList) {
            col.TaskList = new ObservableCollection<TaskCard>(col.TaskList);
            col.ParentBoard = board;
        }

        // TODO: Add safety condition to only save on successful loading of everything
        SaveBoard(board);
        return board;
    }
}
