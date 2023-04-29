using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class IndexModel : PageModel
    {


        public List<TeamInfo> teams =  new List<TeamInfo>();    

        public void OnGet()
        {

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM teams";
                    using (SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           while(reader.Read())
                            {
                                TeamInfo info = new TeamInfo();
                                info.id = "" + reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.teamValue = reader.GetString(2);
                                info.squadSize = "" + reader.GetInt32(3);
                                info.stadium = reader.GetString(4);
                                info.city = reader.GetString(5);
                                info.idLeague = "" + reader.GetInt32(6);

                                teams.Add(info);
                            }
                        }
                    }

                }

            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());  
            }

        }
    }


    public class TeamInfo
    {

        public String id;
        public String name;
        public String teamValue;
        public String squadSize;
        public String stadium;
        public String city;
        public String idLeague;

    }


}
