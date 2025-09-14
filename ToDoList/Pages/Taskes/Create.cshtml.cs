using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ToDoList.Pages.Taskes
{
    public class CreateModel : PageModel
    {
        
        public TaskesInfo taskesInfo = new TaskesInfo();
        public String errromasseg = "";
        public String sucssesmaseg = "";
        
        public void OnGet()
        {
        }

        public void OnPost()
        {
            
            taskesInfo = new TaskesInfo();

            taskesInfo.task_name = Request.Form["task_name"];
            taskesInfo.description = Request.Form["description"];
            taskesInfo.task_status = Request.Form["task_status"];
            taskesInfo.priority_setting = Request.Form["priority_setting"];

            // ›Ì Õ«· ﬂ«‰  «·ÕﬁÊ· ›«—€…
            if (string.IsNullOrWhiteSpace(taskesInfo.task_name) ||
                string.IsNullOrWhiteSpace(taskesInfo.description) ||
                string.IsNullOrWhiteSpace(taskesInfo.task_status) ||
                string.IsNullOrWhiteSpace(taskesInfo.priority_setting))
            {
                errromasseg = "Fill in the empty fields";
                return;
            }

            try
            {
                
                String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Furom2022;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Tasks " +
                                 "(task_name, description, task_status, priority_setting) VALUES " +
                                 "(@task_name, @description, @task_status, @priority_setting);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.Parameters.AddWithValue("@task_name", taskesInfo.task_name);
                        command.Parameters.AddWithValue("@description", taskesInfo.description);
                        command.Parameters.AddWithValue("@task_status", taskesInfo.task_status);
                        command.Parameters.AddWithValue("@priority_setting", taskesInfo.priority_setting);

                        command.ExecuteNonQuery();
                    }
                }

                // Â⁄‰œ ‰Ã«Õ «·⁄„·Ì…
                sucssesmaseg = "The operation was successful";
                Response.Redirect("/Taskes/Index");
            }
            catch (Exception ex)
            {
                
                errromasseg = ex.Message;
                
            }
        }
    }
}