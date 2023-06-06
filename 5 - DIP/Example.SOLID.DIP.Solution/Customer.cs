using System;
using Example.SOLID.DIP.Solution.Interfaces;

namespace Example.SOLID.DIP.Solution
{
    public class Customer
    {
        private readonly IIdentificationServices _identificationServices;
        private readonly IEmailServices _emailServices;

        public Customer(
            IIdentificationServices identificationServices, 
            IEmailServices emailServices)
        {
            _identificationServices = identificationServices;
            _emailServices = emailServices;
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Identification { get; set; }
        public DateTime CreateDate { get; set; }

        public bool IsValid()
        {
            return _emailServices.IsValid(Email) && _identificationServices.IsValid(Identification);
        }
    }
}