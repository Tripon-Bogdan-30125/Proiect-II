using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class CreateModel : PageModel
    {
        public LeagueInfo info = new LeagueInfo();
        public String errorMessage = "";
        public String succesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            info.id = Request.Form["id"];   
            info.name = Request.Form["name"];
            info.country = Request.Form["country"];
            info.marketValue = Request.Form["marketValue"];
            info.numberOfTeams = Request.Form["numberOfTeams"];
            info.ranking = Request.Form["ranking"];
            


            if(info.id.Length == 0 || info.name.Length == 0 || info.country.Length == 0 || info.marketValue.Length == 0 || info.numberOfTeams.Length == 0 || info.ranking.Length == 0)
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
                    String sql = "SET IDENTITY_INSERT [league] ON INSERT INTO league " + "(id, name,country, marketValue, numberOfTeams, ranking) VALUES " + "(@id, @name,@country, @marketValue, @numberOfTeams, @ranking)";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@id", info.id);
                        command.Parameters.AddWithValue("@name", info.name);
                        command.Parameters.AddWithValue("@country",info.country);
                        command.Parameters.AddWithValue("@marketValue", info.marketValue);
                        command.Parameters.AddWithValue("@numberOfTeams", info.numberOfTeams);
                        command.Parameters.AddWithValue("@ranking", info.ranking);
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
            info.country = "";
            info.marketValue = "";
            info.numberOfTeams = "";
            info.ranking = "";
            succesMessage = "Team created succesfully";

            Response.Redirect("/Leagues/Index");
            
        }
    }
}
