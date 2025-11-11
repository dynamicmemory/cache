namespace App.Models {

    class Todo {
        public int Number { get; set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }

        /* Constructor for new Todos */
        public Todo(int number, string title, string description = "") {
            Number = number;
            Title = title;
            Description = description;
            Completed = false;
        }

        /* Updates a Todo to the new values passed in */ 
        public void Update(string title, string description) {
            Title = title;
            Description = description;
        }

        /* Marks a Todo as completed */
        public void Complete() {
            Completed = true;
        }

        /* Pretty prints a Todo*/
        public void Print() {
            
            Console.WriteLine($"Task{this.Number}: {this.Title}");
            Console.WriteLine(this.Description);
            Console.WriteLine($"Completed: {this.Completed}");
            Console.WriteLine("--------------------------------");
        }
    }
}

