using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace Example.SOLID.SRP.Violation
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identification { get; set; }
        public DateTime CreateDate { get; set; }

        public string AddCustomer()
        {
            if (!Email.Contains("@"))
                return "Customer with invalid email.";

            if (Identification.Length != 11)
                return "Customer with invalid identification.";


            using (var cn = new SqlConnection())
            {
                var cmd = new SqlCommand();

                cn.ConnectionString = "SolutionConnectionString";
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO CUSTOMER (NAME, EMAIL IDENTIFICATION, CREATEDATE) VALUES (@name, @email, @identification, @createDate))";

                cmd.Parameters.AddWithValue("name", Name);
                cmd.Parameters.AddWithValue("email", Email);
                cmd.Parameters.AddWithValue("identification", Identification);
                cmd.Parameters.AddWithValue("createDate", CreateDate);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            var mail = new MailMessage("contact@company.com", Email);
            var client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.google.com"
            };

            mail.Subject = "Welcome";
            mail.Body = "Congratulations! You are registered.";
            client.Send(mail);

            return "Customer successfully registered!";
        }
    }
}