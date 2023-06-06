using Example.SOLID.ISP.Solution.Interfaces;

namespace Example.SOLID.ISP.Solution
{
    public class CustomerRegistration : ICustomerRegistration
    {
        public void ValidateData()
        {
            // Validate Identification, Email
        }

        public void SaveBank()
        {
            // Insert into the Customer table
        }

        public void SendEmail()
        {
            // Send email to customer
        }
    }
}