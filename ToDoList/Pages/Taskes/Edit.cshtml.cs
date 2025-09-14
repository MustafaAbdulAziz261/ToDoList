using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ToDoList.Pages.Taskes
{
    public class EditModel : PageModel
    {
        public TaskesInfo taskesInfo = new TaskesInfo();
        public String errromasseg = "";
        public String sucssesmaseg = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Furom2022;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Tasks WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                taskesInfo.id = "" + reader.GetInt32(0);
                                taskesInfo.task_name = reader.GetString(1);
                                taskesInfo.description= reader.GetString(2);
                                taskesInfo.task_status = reader.GetString(3);
                                taskesInfo.priority_setting = reader.GetString(4);
                                
                            }

                        }
                    }

                }               

            }
            catch (Exception ex)
            { 
                errromasseg =ex.Message;
            }
        }
        public void OnPost()
        {
            taskesInfo.id = Request.Form["id"];
            taskesInfo.task_name = Request.Form["task_name"];
            taskesInfo.description = Request.Form["description"];
            taskesInfo.task_status = Request.Form["task_status"];
            taskesInfo.priority_setting = Request.Form["priority_setting"];

            if (string.IsNullOrWhiteSpace(taskesInfo.id) || 
                string.IsNullOrWhiteSpace(taskesInfo.task_name) ||
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
                    String sql = "UPDATE Tasks " +
                                 "SET task_name=@task_name, description=@description, task_status=@task_status, priority_setting=@priority_setting " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", taskesInfo.id); 
                        command.Parameters.AddWithValue("@task_name", taskesInfo.task_name);
                        command.Parameters.AddWithValue("@description", taskesInfo.description);
                        command.Parameters.AddWithValue("@task_status", taskesInfo.task_status);
                        command.Parameters.AddWithValue("@priority_setting", taskesInfo.priority_setting);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errromasseg = ex.Message;
                return;
            }
            Response.Redirect("/Taskes/Index");
        }
    }
}
