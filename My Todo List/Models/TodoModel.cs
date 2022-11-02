namespace My_Todo_List.Models
{
    public class TodoModel
    {
        public int Id { get; set; } 
        public string TaskName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TaskStatus { get; set; }  
    }
}
