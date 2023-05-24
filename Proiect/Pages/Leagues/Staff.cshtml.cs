using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Proiect.Pages.Leagues
{
    public class StaffModel : PageModel
    {

		public List<StaffInfo> staffList = new List<StaffInfo>();
        public void OnGet()
        {
			try
			{
				String id = Request.Query["id"];
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM staff WHERE idTeam=@id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("id", id);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								StaffInfo info = new StaffInfo();
								info.id = "" + reader.GetInt32(0);
								info.name = reader.GetString(1);
								info.nationality = reader.GetString(2);
								info.age = "" + reader.GetInt32(3);
								info.coachingLicense =  reader.GetString(4);
								info.role = reader.GetString(5);
								info.idTeam = "" +  reader.GetInt32(6);
								info.internationalStatus = "" + reader.GetInt32(7);
								info.idNationalTeam = "" + reader.GetInt32(8);

								staffList.Add(info);
							}
						}
					}
					//Console.WriteLine(staffList);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
    }
    public class StaffInfo
    {
        public String id;
        public String name;
        public String nationality;
        public String age;
        public String coachingLicense;
        public String role;
        public String idTeam;
        public String internationalStatus;
		public String idNationalTeam;
	}

}
