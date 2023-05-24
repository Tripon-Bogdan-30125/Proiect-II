using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.AccessControl;

namespace Proiect.Pages.Leagues
{
    public class EditModel : PageModel
    {
        public LeagueInfo info = new LeagueInfo();
		public SecurityInfo securityInfo = new SecurityInfo();
        public String errorMessage = "";
        public String succesMessage = "";
		public String errorMessage1 = "";

		public void OnGet()
		{
			String id = Request.Query["id"];
			try 
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM league WHERE id=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if(reader.Read())
							{							
								info.id = "" + reader.GetInt32(0);
								info.name = reader.GetString(1);
								info.country = reader.GetString(2);
								info.marketValue =  reader.GetString(3);
								info.numberOfTeams = "" + reader.GetInt32(4);
								info.ranking = "" + reader.GetInt32(5);
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

			String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				String sql = "SELECT * FROM Securitate";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						string username = Request.Form["username"];
						string password = Request.Form["password"];
						int gasit = 0;
						while (reader.Read())
						{

							securityInfo.Username = reader.GetString(0);
							securityInfo.Password = reader.GetString(1);
							if (securityInfo.Username == username && securityInfo.Password == password)
							{
								gasit = 1;
							}
							if (gasit == 1)
							{
								break;
							}
						}
						securityInfo.Username = "";
						securityInfo.Password = "";
						if (gasit == 0)
						{
							errorMessage1 = "This user does not have access";
							return;
						}
					}

				}
				connection.Close();
			}


			info.id = Request.Form["id"];
			info.name = Request.Form["name"];
			info.country = Request.Form["country"];
			info.marketValue = Request.Form["marketValue"];
			info.numberOfTeams = Request.Form["numberOfTeams"];
			info.ranking = Request.Form["ranking"];


			if (info.id.Length == 0 || info.name.Length == 0 || info.country.Length == 0 || info.marketValue.Length == 0 || info.numberOfTeams.Length == 0 || info.ranking.Length == 0)
			{
				errorMessage = "All fields must be completed";
				return;
			}
			else

			try
			{
				
				String connectionString1 = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString1))
				{
					connection.Open();
					String sql = "UPDATE league "
						+ "SET name=@name, country=@country, marketValue=@marketValue, numberOfTeams=@numberOfTeams, ranking=@ranking " +
						"WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", info.name);
						command.Parameters.AddWithValue("@country", info.country);
						command.Parameters.AddWithValue("@marketValue", info.marketValue);
						command.Parameters.AddWithValue("@numberOfTeams", info.numberOfTeams);
						command.Parameters.AddWithValue("@ranking", info.ranking);
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
