using System;
using System.Text.Json;
using System.IO;
using App.Models;
using App.ViewModels;
using System.Linq;

public static class JsonDB {
    private const string FilePath = "board.json";

    public static void SaveBoard(BoardViewModel boardVm) {

        // sync all all columns from model
        // boardVm.BoardModel.Columns.Clear();
        // foreach (var colVm in boardVm.Columns) {
            // Sync column name and tasks
            // colVm.ColumnModel.ColumnName = colVm.ColumnName;
            // colVm.ColumnModel.Tasks.Clear();
            // colVm.ColumnModel.Tasks.AddRange(colVm.Tasks.Select(t => t.TaskCardModel));
            //
        //     boardVm.BoardModel.Columns.Add(colVm.ColumnModel);
        // }

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

            // Convert to ViewModels
            BoardViewModel boardVm = new BoardViewModel(board);
            // boardVm.Columns.Clear();
            // foreach (Column col in board.Columns) {
            //     ColumnViewModel colVm = boardVm.CreateCVM(col); 
                // boardVm.Columns.Add(colVm);

                // foreach (TaskCard tc in .Tasks) {
                //     TaskCardViewModel tcVm = new TaskCardViewModel(tc);
                    // colVm.Tasks.Add(tcVm);
            // }

            return boardVm;
        }
        catch (Exception e) {
            Console.WriteLine($"Error loading board: {e.Message}");
            return new BoardViewModel(new Board());
        }
        // Save the new board 

    }
}
