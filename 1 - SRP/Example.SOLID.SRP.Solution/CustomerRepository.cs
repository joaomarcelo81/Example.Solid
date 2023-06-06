using System.Data;
using System.Data.SqlClient;

namespace Example.SOLID.SRP.Solution
{
    public class CustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            using (var cn = new SqlConnection())
            {
                var cmd = new SqlCommand();

                cn.ConnectionString = "SolutionConnectionString";
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO CUSTOMER (NAME, EMAIL IDENTIFICATION, CREATEDATE) VALUES (@name, @email, @identification, @createDate))";

                cmd.Parameters.AddWithValue("name", customer.Name);
                cmd.Parameters.AddWithValue("email", customer.Email);
                cmd.Parameters.AddWithValue("identification", customer.Identification);
                cmd.Parameters.AddWithValue("createDate", customer.CreateDate);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}