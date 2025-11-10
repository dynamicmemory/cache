namespace App.Models {

    class Todo {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }

        /* Constructor for new Todos */
        public Todo(string title, string description = "") {
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

        public void Print() {
            Console.WriteLine($"Task: {this.Title}");
            Console.WriteLine(this.Description);
            Console.WriteLine($"Completed: {this.Completed}");
            Console.WriteLine("--------------------------------");
        }
    }
}

