using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data.SqlClient;
namespace ToDoList.Pages.Taskes
{
    public class IndexModel : PageModel
    {
        public List<TaskesInfo> listTaskes =new List<TaskesInfo>();
        public void OnGet()
        {
            try
            {
                // to concet database yaaaa stooof
                String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Furom2022;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                { 
                    connection.Open();
                    String sql = "SELECT * FROM Tasks";

                    using (SqlCommand command = new SqlCommand(sql, connection))

                    {
                        using (SqlDataReader reder = command.ExecuteReader())
                        {
                            while (reder.Read()) 
                            { 
                                TaskesInfo taskesInfo = new TaskesInfo();
                                taskesInfo.id = "" + reder.GetInt32(0);
                                taskesInfo.task_name = reder.GetString(1);
                                taskesInfo.description = reder.GetString(2);
                                taskesInfo.task_status = reder.GetString(3);
                                taskesInfo.priority_setting = reder.GetString(4);
                                taskesInfo.created_at = reder.GetDateTime(5).ToString();


                                listTaskes.Add(taskesInfo);

                            }

                        }
                    }
                }
            } 
            catch (Exception ex)
            {
            Console.WriteLine("erorrr ya fandeeem "+ex.ToString());
            
            }
            
        }
    }

    public class TaskesInfo
    {
        public string id;
        public string task_name;
        public string description;
        public string task_status;
        public string priority_setting;
        public string created_at;
    }
}
