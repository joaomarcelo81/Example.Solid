using System;

namespace Example.SOLID.ISP.Violation
{
    public class ProductRegistration : IRegistration
    {
        public void ValidateData()
        {
            // validate value

        }

        public void SaveBank()
        {
            // Insert Product table

        }

        public void SendEmail()
        {
            // Product does not have email, what do I do now???
            throw new NotImplementedException("This method is useless.");
        }
    }
}