namespace App.ViewModels;

public class MainViewModel {
    public BoardViewModel Board { get; }

    public MainViewModel() {
        Board = new BoardViewModel();
    }
}
