using Microsoft.AspNetCore.Mvc;
using My_Todo_List.Models;
using My_Todo_List.Views.ViewModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace My_Todo_List.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var TodoListViewMode = GetAllTodos();

            return View(TodoListViewMode);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        internal ToDoViewModel GetAllTodos()
        {
            List<TodoModel> todoList = new();
            using(SqlConnection con = new SqlConnection(@"Data Source =desktop-iu46u34\express2; Initial Catalog=ToDoList; Integrated Security = True"))
            {
                using (var tablecmd = con.CreateCommand())
                {
                    con.Open();
                    tablecmd.CommandText = $"SELECT * FROM todolist1";

                    using(var reader = tablecmd.ExecuteReader())
                    {
                       if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                todoList.Add(new TodoModel
                                {
                                    Id = reader.GetInt32(0),
                                    TaskName = reader.GetString(1),
                                    TaskStatus = reader.GetString(2),
                                    CreatedDate = reader.GetDateTime(3),
                                });
                            }
                        }
                       else
                        {
                            return new ToDoViewModel
                            {
                                TodoList = todoList
                            };
                           
                        }

                    }
                     
                }
            }
            return new ToDoViewModel
            {
                TodoList = todoList
            };
        }
       
        public RedirectResult Insert(TodoModel todo)
        {
            using(SqlConnection con = new SqlConnection(@"Data Source =desktop-iu46u34\express2; Initial Catalog=ToDoList; Integrated Security = True"))
            {
                using (var tablecom = con.CreateCommand())
                {
                    con.Open();
                    tablecom.CommandText = $"INSERT into todolist1 VALUES ({todo.TaskName}, {todo.CreatedDate}, {todo.TaskStatus})";
                    try
                    {
                        tablecom.ExecuteNonQuery();
                    }  
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return Redirect("https://localhost:7121");
        }
       

    }
}