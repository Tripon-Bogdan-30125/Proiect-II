using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class IndexModel : PageModel
    {


        public List<LeagueInfo> leagues =  new List<LeagueInfo>();    

        public void OnGet()
        {

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM league";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           while(reader.Read())
                            {
                                LeagueInfo info = new LeagueInfo();
                                info.id = "" + reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.country = reader.GetString(2);
                                info.marketValue =  reader.GetString(3);
                                info.numberOfTeams = "" + reader.GetInt32(4);
                                info.ranking = "" + reader.GetInt32(5);

                                leagues.Add(info);
                            }
                        }
                    }
                    Console.WriteLine(leagues);
                }

            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());  
            }

        }
    }


    public class LeagueInfo
    {

        public String id;
        public String name;
        public String country;
        public String marketValue;
        public String numberOfTeams;
        public String ranking;
    }
}
