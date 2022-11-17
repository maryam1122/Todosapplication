
using Todolistapplication.Models;

namespace Todolistapplication.Interface
{
    public interface ITodolist
    {
        public List<TodoItem> GetTodoItemDetails();
        public TodoItem GetTodoItemDetails(int id);
        public void AddTodoItem(TodoItem item);
        public void UpdateTodoItem(TodoItem item);
        public TodoItem DeleteTodoItem(int Id);
        


    }
}
