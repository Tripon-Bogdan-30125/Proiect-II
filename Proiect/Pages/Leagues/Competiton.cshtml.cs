using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
	public class CompetitonModel : PageModel
	{

		public List<CompetitonInfo> competition = new List<CompetitonInfo>();
		public void OnGet()
		{

			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM competition";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								CompetitonInfo info = new CompetitonInfo();
								info.id = "" + reader.GetInt32(0);
								info.name = reader.GetString(1);
								info.numberOfTeams = "" + reader.GetInt32(2);
								info.marketValue = reader.GetString(3);

								competition.Add(info);
							}
						}
					}
					Console.WriteLine(competition);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}

		}
	}


	public class CompetitonInfo
	{
		public String id;
		public String name;
		public String numberOfTeams;
		public String marketValue;
	}
}

