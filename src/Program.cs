using App.Managers;

public class Program {

    public static void Main() {
       
        TodoManager manager = new TodoManager();
        MenuManager menu = new MenuManager(manager);

        // Main loop of the program 
        while (true) {
            Console.Clear();
            manager.PrintAll();
            if (menu.Run() == 0) break;
        }
    }
}
