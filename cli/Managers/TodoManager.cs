using System.Text.Json;
using App.Models; 

namespace App.Managers {

    class TodoManager {
        private List<Todo> todos  = new List<Todo>();
        private string filePath = "todos.json";

        public TodoManager() {
            LoadList();
        }

        /* Loads the json "database" into a list of Todos for display*/ 
        public void LoadList() {
            if (!File.Exists(filePath)) return;

            try {
                string json = File.ReadAllText(filePath);
                todos = JsonSerializer.Deserialize<List<Todo>>(json) ?? new List<Todo>();
                UpdateListNumbers();
            } 
            catch (Exception e) {
                Console.WriteLine("Failed to load todos: " + e.Message);
            }
        }

        /* Saves the list of Todos to the json "database"*/ 
        public void SaveList() {
            try {
                string json = JsonSerializer.Serialize(
                        todos, 
                        new JsonSerializerOptions { WriteIndented=true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception e) {
                Console.WriteLine("Failed to save todos: " + e.Message);
            }
        }

        /* Generates a new Todo from the passed in title and description and 
         * adds it to the total list of Todos */ 
        public void AddTodo(string title, string description) {
            int number = TodoNumber();
            Todo t = new Todo(number, title, description);
            todos.Add(t);
            SaveList();
        }

        /* Removes the selected Todo from the list, updates the numbers for each 
         * todo to maintain correct ordering */
        public void RemoveTodo(int n) {
            // Protect from deleting from an empty list
            if (todos.Count < 1) return;

            Todo selectedTodo = SelectedTodo(n);
            todos.Remove(selectedTodo);
            UpdateListNumbers();
            SaveList();
        }

        /* Edits a todo from the list with the passed in title and description*/
        public void EditTodo(string title, string desc, int n) {
            // Protect from deleting from an empty list
            if (todos.Count < 1) return;
            Todo selected = SelectedTodo(n);

            if (title == "") title = selected.Title; 
            if (desc == "") desc = selected.Description; 

            selected.Update(title, desc);
            SaveList();
        }

        /* Prints all the todos from the list */
        public void PrintAll() {
            // incase the list is empty, print as such to the user
            if (todos.Count == 0) {
                Console.WriteLine("No tasks.");
                return;
            }

            foreach (var todo in todos)
                todo.Print();
        }

        // TODO: This is a horrible way to do this, simplify and streamline it.
        /* Gets the number of todos that are currently stored in the list */
        public int TodoNumber() {
            int size = todos.Count+1; // +1 as users see the first task as 1 not 0
            return size;
        }

        /* Returns the selected todo by looking up the passed in number in 
           the list adding one and returning the todo at that index */
        public Todo SelectedTodo(int n) {
            return todos[n-1];     // -1 as users see the first task as 1 not 0
        }

        /* Runs after any change occurs to the list, updates all the numbers to 
         * ensure the ordering stays correct.*/
        public void UpdateListNumbers() {
            for (int i=0; i<todos.Count; i++) {
                todos[i].Number = i+1;  // +1 as User sees todos starting from 1 not 0
            }
        }
    }
}
