using System;
using App.Models;
using App.Managers;

public class Program {

    public static void Main() {
        // Create the new todos list
        TodoManager manager = new TodoManager();


        manager.AddTodo("Learn C#", "Start by building a cli CRUD app in C#");
        manager.AddTodo("Build Latent in C#", "The ERP Framework of the future");
         
        while (true) {
            Console.Clear();
            manager.PrintAll();
            Console.WriteLine("Enter a command:");
            Console.WriteLine(
                    "(a) for add\n(e) for edit\n(d) for delete\n(q) for quit");
            string command = Console.ReadLine();

            if (command == "a") {
                Console.Write("Enter the title");
                string title = Console.ReadLine();
                Console.Write("Enter the description");
                string description = Console.ReadLine();
                manager.AddTodo(title, description);
            }
            else if  (command == "q") {
                break;
            }
        }

    }
}

