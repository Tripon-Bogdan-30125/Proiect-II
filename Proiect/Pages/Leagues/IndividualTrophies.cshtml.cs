using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Proiect.Pages.Leagues
{
    public class IndividualTrophiesModel : PageModel
    {
        public List<IndividualTrophiesInfo> lista_trofeeInd = new List<IndividualTrophiesInfo>();

        public void OnGet()
        {
			try
			{
				String id = Request.Query["id"];
				String connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM individualTrophies JOIN playerToTrophies ON individualTrophies.id = playerToTrophies.idTrophy JOIN players ON players.id = playerToTrophies.idPlayer AND players.id = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{

							while (reader.Read())
							{
								IndividualTrophiesInfo trofeu_individual = new IndividualTrophiesInfo();
								trofeu_individual.id = "" + reader.GetInt32(0);
								trofeu_individual.name = reader.GetString(1);

								lista_trofeeInd.Add(trofeu_individual);
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
    public class IndividualTrophiesInfo
    {
        public String id;
        public String name;

    }
}
