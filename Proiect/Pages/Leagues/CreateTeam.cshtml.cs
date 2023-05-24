using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class CreateTeamModel : PageModel
    {
		public TeamInfo info = new TeamInfo();
		public SecurityInfo securityInfo = new SecurityInfo();	
		public String errorMessage = "";
		public String errorMessage1 = "";
		public String succesMessage = "";
		public void OnGet()

		{ }

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

			//info.id = Request.Form["id"];
			info.name = Request.Form["name"];
			info.teamValue = Request.Form["teamValue"];
			info.squadSize = Request.Form["squadSize"];
			info.stadium = Request.Form["stadium"];
			info.city = Request.Form["city"];
			info.idLeague = Request.Form["idLeague"];


			if (info.name.Length == 0 || info.teamValue.Length == 0 || info.squadSize.Length == 0 || info.stadium.Length == 0 || info.city.Length == 0 || info.idLeague.Length == 0)
			{
				errorMessage = "All fields must be completed";
				return;
			}


			try
			{

				String connectionString1 = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection1 = new SqlConnection(connectionString1))
				{
					connection1.Open();
					String sql1 = "SELECT MAX(id) FROM [teams]";
					SqlCommand cmd = new SqlCommand(sql1, connection1);
					int maxId = (int)cmd.ExecuteScalar();

					sql1 = "SET IDENTITY_INSERT [teams] ON INSERT INTO teams " + "(id, name,teamValue, squadSize, stadium, city, idLeague) VALUES " + "(@id, @name,@teamValue, @squadSize, @stadium, @city, @idLeague)";
					using (SqlCommand command = new SqlCommand(sql1, connection1))
					{
						command.Parameters.AddWithValue("@id", maxId+1);
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
			catch (Exception ex)
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