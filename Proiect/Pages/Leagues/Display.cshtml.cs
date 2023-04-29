using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class DisplayModel : PageModel
    {

       public List<PlayerInfo> PlayerList = new List<PlayerInfo>();
		public void OnGet()
		{
			try
			{ 
				String id = Request.Query["id"];
				String connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();	
					String sql = "SELECT * FROM players WHERE idTeam = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							
							while (reader.Read())
							{
								PlayerInfo player1 = new PlayerInfo();
								player1.id = "" + reader.GetInt32(0);
								player1.name = reader.GetString(1);
								player1.nationality = reader.GetString(2);
								player1.age = "" + reader.GetInt32(3);
								player1.position = reader.GetString(4);
								player1.height = reader.GetString(5);
								player1.foot = reader.GetString(6);
								player1.playerValue = reader.GetString(7);
								player1.internationalStatus = reader.GetString(8);
								player1.outfitter = reader.GetString(9);
								player1.idTeam = "" + reader.GetInt32(10);
								player1.idNationalTeam = "" + reader.GetInt32(11);

								PlayerList.Add(player1);
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


    public class PlayerInfo
    {
        public String id;
        public String name;
        public String nationality;
        public String age;
        public String position;
        public String height;
        public String foot;
        public String playerValue;
        public String internationalStatus;
        public String outfitter;
        public String idTeam;
        public String idNationalTeam;
    }
}
