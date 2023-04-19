using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace Proiect.Pages.Leagues
{
    public class IndexModel : PageModel
    {

        public List<teams> listTeam = new List<teams>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT name, teamValue, squadSize, stadium, city FROM teams WHERE teams(idLeague) == league(id) && league(name) == @name";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    String leagueName = Request.Form["leagueName"];
                    cmd.Parameters.AddWithValue("@name",leagueName);
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                teams teamInfo = new teams();
                                teamInfo.name = reader.GetString(2); 
                                teamInfo.teamValue ="" + reader.GetSqlMoney(3);
                                teamInfo.squadSize = "" + reader.GetInt32(4);
                                teamInfo.stadium = reader.GetString(5);
                                teamInfo.city = reader.GetString(6);

                                listTeam.Add(teamInfo);

                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }


        public class teams
        {
            public String name;
            public String teamValue;
            public String squadSize;
            public String stadium;
            public String city;

		}
    }
}
