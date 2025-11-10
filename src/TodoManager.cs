using App.Todo;

namespace App.TodoManager {

    class TodoManager {
        public List<Todo> todos  = new List<Todo>();

        public TodoManager() {
                    
        }

        public void AddTodo(Todo t) => todos.Add(t);

        public void RemoveTodo(Todo t) => todos.Remove(t);

        public void PrintAll() {
            foreach (var todo in todos)
                todo.Print();
        }
    }
}
