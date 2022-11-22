using Todolistapplication.Models;

namespace Todolistapplication.Interface
{
    public interface ITodolist
    {
        public Task<List<TodoItem>> GetTodoItemDetailsByStatusAsync(Status status);
        public Task<List<TodoItem>> GetTodoItemDetailsAsync();
        public Task<TodoItem> GetTodoItemDetailsAsync(int id);
        public void AddTodoItem(TodoItem item);
        public void UpdateTodoItem(TodoItem item);
        public TodoItem DeleteTodoItem(int Id);
    }
}
