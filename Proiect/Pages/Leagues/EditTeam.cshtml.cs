using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
	public class EditTeamModel : PageModel
	{
		public TeamInfo info = new TeamInfo();
		public SecurityInfo securityInfo = new SecurityInfo();
		public String errorMessage1 = "";
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
							if (reader.Read())
							{
								info.id = "" + reader.GetInt32(0);
								info.name = reader.GetString(1);
								info.teamValue= reader.GetString(2);
								info.squadSize ="" + reader.GetInt32(3);
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
			info.teamValue = Request.Form["teamValue"];
			info.squadSize = Request.Form["squadSize"];
			info.stadium = Request.Form["stadium"];
			info.city = Request.Form["city"];


			if (info.id.Length == 0 || info.name.Length == 0 || info.teamValue.Length == 0 || info.squadSize.Length == 0 || info.stadium.Length == 0 || info.city.Length == 0 )
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
					String sql1 = "UPDATE teams "
						+ "SET name=@name, teamValue=@teamValue, squadSize=@squadSize, stadium=@stadium, city=@city " +
						" WHERE id=@id";

					using (SqlCommand command = new SqlCommand(sql1, connection1))
					{
						command.Parameters.AddWithValue("@name", info.name);
						command.Parameters.AddWithValue("@teamValue", info.teamValue);
						command.Parameters.AddWithValue("@squadSize", info.squadSize);
						command.Parameters.AddWithValue("@stadium", info.stadium);
						command.Parameters.AddWithValue("@city", info.city);
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
