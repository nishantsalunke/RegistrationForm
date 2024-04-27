using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RegForm.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo =new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        //For Reading 
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=RegForm;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.firstName = reader.GetString(1);
                                clientInfo.lastName = reader.GetString(2);
                                clientInfo.email = reader.GetString(3);
                                clientInfo.phone = reader.GetString(4);
                                clientInfo.dob = reader.GetDateTime(5).ToString();
                                clientInfo.address = reader.GetString(6);

                                clientInfo.dob = reader.GetDateTime(5).ToShortDateString();

                                clientInfo.state = reader.GetString(7);
                                clientInfo.city = reader.GetString(8);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

        }


        //For Updating
        public void OnPost() {

            clientInfo.id = Request.Form["id"];
            clientInfo.firstName= Request.Form["firstname"];
            clientInfo.lastName = Request.Form["lastName"];
            clientInfo.email= Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.dob = Request.Form["dob"];
            clientInfo.address= Request.Form["address"];
            clientInfo.state = Request.Form["state"];
            clientInfo.city = Request.Form["city"];

            if (clientInfo.firstName.Length == 0 || clientInfo.lastName.Length == 0
               || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 ||
                clientInfo.address.Length == 0 || clientInfo.dob.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=RegForm;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients SET firstName=@firstName, lastName=@lastName," +
                        " email=@email, phone=@phone, dob=@dob, address=@address, state=@state, city=@city "+
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", clientInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", clientInfo.lastName);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@dob", clientInfo.dob);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@state", clientInfo.state);
                        command.Parameters.AddWithValue("@city", clientInfo.city);

                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();


                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            //Redirecting to the list of user page
            Response.Redirect("/Clients/Index");
        }    
    }
}
