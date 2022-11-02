using My_Todo_List.Models;

namespace My_Todo_List.Views.ViewModel
{
    public class ToDoViewModel
    {
        public List<TodoModel> TodoList { get; set; }
        public TodoModel Todo { get; set; }
    }
}
