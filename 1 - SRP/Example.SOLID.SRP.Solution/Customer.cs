using System;

namespace Example.SOLID.SRP.Solution
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identification { get; set; }
        public DateTime CreateDate { get; set; }

        public bool IsValid()
        {
            return EmailServices.IsValid(Email) && IdentificationServices.IsValid(Identification);
        }
    }
}