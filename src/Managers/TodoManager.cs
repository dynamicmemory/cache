using System.Collections.Generic;
using App.Models; 

namespace App.Managers {

    class TodoManager {
        private List<Todo> todos  = new List<Todo>();

        public TodoManager() {
                    
        }

        public void AddTodo(string title, string description) {
            Todo t = new Todo(title, description);
            todos.Add(t);
        }

        public void RemoveTodo(Todo t) => todos.Remove(t);

        public void PrintAll() {
            foreach (var todo in todos)
                todo.Print();
        }
    }
}
