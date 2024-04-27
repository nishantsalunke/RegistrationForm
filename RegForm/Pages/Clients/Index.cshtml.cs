using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace RegForm.Pages.Clients
{
    public class IndexModel : PageModel
    {
        // will store the all Clients
        public List<ClientInfo> listClints = new List<ClientInfo>();

        //HttpGet()  Access to the database and to read the data from the table

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=RegForm;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // String sql = "SELECT * FROM clients";
                    
                    String sql = "SELECT CONCAT(firstName, ' ', lastName) AS FullName, * FROM clients";


                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            // reading the data by using while loop
                            while (reader.Read()) {

                                ClientInfo clientInfo =new ClientInfo();
                                //clientInfo.id = "" + reader.GetInt32(0);   

                                // clientInfo.firstName = reader.GetString(1); 
                                //  clientInfo.lastName = reader.GetString(2);
                                //  clientInfo.email = reader.GetString(3);
                                //  clientInfo.phone = reader.GetString(4);

                                //  clientInfo.dob = reader.GetDateTime(5).ToShortDateString();

                                // clientInfo.address= reader.GetString(6);
                                //clientInfo.state= reader.GetString(7);  
                                // clientInfo.city= reader.GetString(8);

                                //clientInfo.fullName = reader.GetString(1) + " " + reader.GetString(2);  ----------2nd Way

                                clientInfo.fullName = reader["FullName"].ToString();  // Combining first name and last name by concat

                                clientInfo.id = reader.GetInt32(1).ToString();      //Added empty string to convert it into int to string
                                clientInfo.email = reader.GetString(4);
                                clientInfo.phone = reader.GetString(5);

                                // Converted into Only string Date format by ToShorDateString method
                                clientInfo.dob = reader.GetDateTime(6).ToShortDateString();
                                //clientInfo.dob = date;
                                // clientInfo.dob = reader.GetDateTimeOffset(5).ToString();
                                //fromDate = fromDate.Date;

                                clientInfo.address = reader.GetString(7);
                                clientInfo.state = reader.GetString(8);
                                clientInfo.city = reader.GetString(9);

                               

                                // Adding all the clint info in the list
                                listClints.Add(clientInfo);
                                

                            }
                        }
                    }
                }

       


            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception "+ex.ToString());
            }
        }
    }

    // this class stores data of the one client from the database
    public class ClientInfo
    {
        //created variables to store the data

        public String id;
        public String fullName;
        public String firstName;
        public String lastName;
        public String email;
        public String phone;
        public String dob;
        public String address;
        public String state;
        public String city;
    }
}
