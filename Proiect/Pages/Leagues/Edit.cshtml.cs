using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class EditModel : PageModel
    {
        public TeamInfo info = new TeamInfo();
        public String errorMessage = "";
        public String succesMessage = "";
		public void OnGet()
		{
			String id = Request.Query["id"];
			try 
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM teams WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if(reader.Read())
							{							
								info.id = "" + reader.GetInt32(0);
								info.name = reader.GetString(1);
								info.teamValue = reader.GetString(2);
								info.squadSize = "" + reader.GetInt32(3);
								info.stadium = reader.GetString(4);
								info.city = reader.GetString(5);
								info.idLeague = "" + reader.GetInt32(6);
							}
						}
					}

				}
			}
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
            }
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

			if (info.id.Length == 0 || info.name.Length == 0 || info.teamValue.Length == 0 || info.squadSize.Length == 0 || info.stadium.Length == 0 || info.city.Length == 0 || info.idLeague.Length == 0)
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
					String sql = "UPDATE teams "
						+ "SET name=@name, teamValue=@teamValue, squadSize=@squadSize, stadium=@stadium, city=@city, idLeague=@idLeague " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", info.name);
						command.Parameters.AddWithValue("@teamValue", info.teamValue);
						command.Parameters.AddWithValue("@squadSize", info.squadSize);
						command.Parameters.AddWithValue("@stadium", info.stadium);
						command.Parameters.AddWithValue("@city", info.city);
						command.Parameters.AddWithValue("@idLeague", info.idLeague);
						command.Parameters.AddWithValue("@id", info.id);
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex) 
			{
				errorMessage = ex.Message;
				return;
			}
			Response.Redirect("/Leagues/Index");
        }
    }
}
