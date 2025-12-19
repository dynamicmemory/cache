/* The Current solution to persistence, a switch to SQLite or PSQL is possible 
 * in the future after abit of time using and testing the app to see what needs
 * to be changed or added. 
 */
using System;
using System.Text.Json;
using System.IO;
using App.Models;
using App.ViewModels;

public static class JsonDB {
    private const string FilePath = "board.json";

    /* Saves the current Board Model that holds all columns which hold all task
     * cards.
     */
    public static void SaveBoard(BoardViewModel boardVm) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(boardVm.BoardModel, options);
        File.WriteAllText(FilePath, json);
    }

    /* Loads the Board model for the application*/
    public static BoardViewModel LoadBoard() {
        // If started with no database, will create a new board automatically
        if (!File.Exists(FilePath))
            return new BoardViewModel(new Board());

        // If the json is malformed, will currently create a new board to start 
        // again with.
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
