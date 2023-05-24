using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Proiect.Pages.Leagues
{
	public class Delete1Model : PageModel
	{

		public SecurityInfo securityInfo = new SecurityInfo();
		public String errorMessage1 = "";
		public void OnGet()
		{
			
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


			try
			{
				String id = Request.Query["id"];
				String connectionString1 = "Data Source=.\\sqlexpress;Initial Catalog=ourwebsite;Integrated Security=True";
				using (SqlConnection connection1 = new SqlConnection(connectionString1))
				{
					connection1.Open();
					String sql1 = "DELETE FROM league WHERE id=@id";
					using (SqlCommand command1 = new SqlCommand(sql1, connection1))
					{
						command1.Parameters.AddWithValue("@id", id);
						command1.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{

			}
			Response.Redirect("/Leagues/Index");


		}
	}
}
