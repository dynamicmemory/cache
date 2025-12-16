using System;
using System.Text.Json;
using System.IO;
using App.Models;
using App.ViewModels;
using System.Linq;

public static class JsonDB {
    private const string FilePath = "board.json";

    public static void SaveBoard(BoardViewModel boardVm) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(boardVm.BoardModel, options);
        File.WriteAllText(FilePath, json);
    }

    public static BoardViewModel LoadBoard() {
        if (!File.Exists(FilePath))
            return new BoardViewModel(new Board());

        try {
            string json = File.ReadAllText(FilePath);
            Board board = JsonSerializer.Deserialize<Board>(json)!;

            // Convert to ViewModel
            BoardViewModel boardVm = new BoardViewModel(board);
            return boardVm;
        }
        catch (Exception e) {
            Console.WriteLine($"Error loading board: {e.Message}");
            return new BoardViewModel(new Board());
        }
    }
}
