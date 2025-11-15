using App.Managers;

public class Program {

    public static void Main() {
        TodoManager manager = new TodoManager();
        MenuManager menu = new MenuManager(manager);

        while (true) {
            if (menu.Run()) break;
        }
    }
}
