namespace App.ViewModels;

public class MainViewModel {
    public BoardViewModel Board { get; }

    public MainViewModel() {
        try {
            Board = JsonDB.LoadBoard();
        }
        catch {
            Board = new BoardViewModel(new Models.Board());
        }
    }
}
