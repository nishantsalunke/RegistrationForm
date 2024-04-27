using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RegForm.Pages.Clients
{
    public class CreateModel : PageModel
    {
        //
        public ClientInfo clientInfo= new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        //This Onpost method will executed when we send the data using submit button
        //reading the data and putting it in clientinfo object
        public void OnPost() 
        { 
            clientInfo.firstName = Request.Form["firstName"];    
            clientInfo.lastName= Request.Form["lastName"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.dob = Request.Form["dob"];
            clientInfo.address = Request.Form["address"];
            clientInfo.state = Request.Form["state"];
            clientInfo.city = Request.Form["city"];

            // VAlidating Different fields if any empty then it will show the errorMsg

            if (clientInfo.firstName.Length == 0 || clientInfo.lastName.Length == 0
                || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 ||
                 clientInfo.address.Length == 0 || clientInfo.dob.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // Saving The new User in the database

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=RegForm;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients (firstName, lastName, email, phone, dob, address, state, city)" +
                        "VALUES(@firstName, @lastName, @email, @phone, @dob, @address, @state, @city);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", clientInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", clientInfo.lastName);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@dob", clientInfo.dob);
                        command.Parameters.AddWithValue ("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@state", clientInfo.state);
                        command.Parameters.AddWithValue("@city", clientInfo.city);

                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.firstName = ""; 
            clientInfo.lastName = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.dob = "";
            clientInfo.address = "";
            clientInfo.state = "";
            clientInfo.city = "";
             
            successMessage = "New Client Added SuccessFully";

           // Response.Redirect("/Clients/Index");

                
                
        }
    }
}
