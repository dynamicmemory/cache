using System;
using System.Text.Json;
using System.IO;
using App.Models;
using App.ViewModels;
using System.Linq;

public static class JsonDB
{
    private const string FilePath = "board.json";

    public static void SaveBoard(BoardViewModel boardVm)
    {
        // Sync VM -> model
        boardVm.BoardModel.Columns.Clear();
        foreach (var colVm in boardVm.Columns)
        {
            // Sync column name and tasks
            colVm.ColumnModel.ColumnName = colVm.ColumnName;
            colVm.ColumnModel.Tasks.Clear();
            colVm.ColumnModel.Tasks.AddRange(colVm.Tasks.Select(t => t.TaskCardModel));

            boardVm.BoardModel.Columns.Add(colVm.ColumnModel);
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(boardVm.BoardModel, options);
        File.WriteAllText(FilePath, json);
    }

    public static BoardViewModel LoadBoard()
    {
        if (!File.Exists(FilePath))
            return new BoardViewModel();

        try
        {
            string json = File.ReadAllText(FilePath);
            var board = JsonSerializer.Deserialize<Board>(json)!;

            // Convert to ViewModels
            var boardVm = new BoardViewModel();
            boardVm.Columns.Clear();
            foreach (var col in board.Columns)
            {
                var colVm = new ColumnViewModel(col, removable: true);
                boardVm.Columns.Add(colVm);
            }

            return boardVm;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading board: {e.Message}");
            return new BoardViewModel();
        }
    }
}
