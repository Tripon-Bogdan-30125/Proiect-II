using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class CreateModel : PageModel
    {
        public TeamInfo info = new TeamInfo();
        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            info.id = Request.Form["id"];   
            info.name = Request.Form["name"];
            info.teamValue = Request.Form["teamValue"];
            info.squadSize = Request.Form["squadSize"];
            info.stadium = Request.Form["stadium"];
            info.city = Request.Form["city"];
            info.idLeague = Request.Form["idLeague"];


            if(info.id.Length == 0 || info.name.Length == 0 || info.teamValue.Length == 0 || info.squadSize.Length == 0 || info.stadium.Length == 0 || info.city.Length == 0 || info.idLeague.Length == 0)
            {
                errorMessage = "All fields must be completed";
                return;
            }


            try
            {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SET IDENTITY_INSERT [teams] ON INSERT INTO teams " + "(id, name, teamValue, squadSize, stadium, city, idLeague) VALUES " + "(@id, @name, @teamValue, @squadSize, @stadium, @city, @idLeague)";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@id", info.id);
                        command.Parameters.AddWithValue("@name", info.name);
                        command.Parameters.AddWithValue("@teamValue", info.teamValue);
                        command.Parameters.AddWithValue("@squadSize", info.squadSize);
                        command.Parameters.AddWithValue("@stadium", info.stadium);
                        command.Parameters.AddWithValue("@city", info.city);
                        command.Parameters.AddWithValue("@idLeague", info.idLeague);
                        command.ExecuteNonQuery();
					}

                }
			}
            catch(Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }


            info.name = "";
            info.teamValue = "";
            info.squadSize = "";
            info.stadium = "";
            info.city = "";
            succesMessage = "Team created succesfully";

            Response.Redirect("/Leagues/Index");
            
        }
    }
}
