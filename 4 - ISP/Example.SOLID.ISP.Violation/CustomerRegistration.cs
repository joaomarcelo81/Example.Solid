using System.Collections.Generic;

namespace Example.SOLID.ISP.Violation
{
    public class CustomerRegistration : IRegistration
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