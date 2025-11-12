namespace App.Managers {

    class MenuManager {
        private TodoManager Manager { get; set; }
        private bool State { get; set;}
    
        public MenuManager(TodoManager manager) {
            Manager = manager; 
            State = false;
        }

        /* Handles input commands from the user and returns the state of the 
         * program after each command, 1 for active, 0 for quit*/
        public bool Run() {
            Console.Clear();
            Manager.PrintAll();

            Console.WriteLine("Enter a command:");
            Console.WriteLine(
                    "(a) for add\n(e) for edit\n(d) for delete\n(q) for quit");
            Console.Write("\n>> ");
            string command = Console.ReadLine() ?? "";
            CommandManager(command);
            return State;
        }

        /* Pattern matches a users command and hands off control to the chosen
         * function */
        public void CommandManager(string command) {
            var action = command switch {
                "a" => (Action)Add,    // Take note of the cast for future
                "e" => Edit,
                "d" => Delete,
                "q" => Quit,
                _ => UnknownCommand
            };
            action();
        }

        /* Control flow for adding a new Task to the list */
        public void Add() {
            Console.Write("Enter the title");
            string title = Console.ReadLine() ?? "";
            Console.Write("Enter the description");
            string description = Console.ReadLine() ?? "";
            Manager.AddTodo(title, description);
        }

        /* Control flow for editing a Task in the list */
        public void Edit() {
            int number = VerifyNumber();
            Console.Write("Change the title to (leave blank to keep)? ");
            string title = Console.ReadLine() ?? "";
            Console.Write("Change the description to (leave blank to keep)? ");
            string description = Console.ReadLine() ?? "";
            Manager.EditTodo(title, description, number);
        }

        /* Removes the selected task from the list.*/
        public void Delete() {
            Manager.RemoveTodo(VerifyNumber());
        }

        /* Flips the state of the program to 0 i.e false for the main loop to 
         * exit.*/
        public void Quit() {
            State = true;
        }

        /* Control for unknown input, currently does nothing as prompt is just 
         * repeated for the user at the moment*/
        public void UnknownCommand() {
            return;
        }

        /* Ensures the user inputs a correct number that corresponds to a task 
           in the list */ 
        public int VerifyNumber() {
            while (true) {
                Console.Write("Which Task do you wish to select: ");
                string selection = Console.ReadLine() ?? "";
                try {
                    int number = Convert.ToInt32(selection);
                    if (number > Manager.TodoNumber()) {
                        Console.WriteLine("No task of that number exits");
                    }
                    else return number;
                }
                catch (Exception) {
                    Console.WriteLine($"{selection} is not a valid task number.");
                }
            }
        }
    }
}
