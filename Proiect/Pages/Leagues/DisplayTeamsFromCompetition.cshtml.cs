using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class DisplayTeamsFromCompetitionModel : PageModel
    {
        public List<TeamInfo> teamList = new List<TeamInfo>();
		public void OnGet()
		{
			try
			{
				String id = Request.Query["id"];
				String connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM teams JOIN teamToCompetition ON teams.id = teamToCompetition.idTeam JOIN competition ON competition.id = teamToCompetition.idCompetition AND competition.id = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							while (reader.Read())
							{
								TeamInfo team = new TeamInfo();
								team.id = "" + reader.GetInt32(0);
								team.name = reader.GetString(1);
								team.teamValue = reader.GetString(2);
								team.squadSize = "" + reader.GetInt32(3);
								team.stadium = reader.GetString(4);
								team.city = reader.GetString(5);
								team.idLeague = "" + reader.GetInt32(6);

								teamList.Add(team);
							}

						}
					}
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
	}
}
