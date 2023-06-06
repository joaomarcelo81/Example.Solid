using Example.SOLID.DIP.Solution.Interfaces;

namespace Example.SOLID.DIP.Solution
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailServices _emailServices;

        public CustomerServices(
            IEmailServices emailServices, 
            ICustomerRepository CustomerRepository)
        {
            _emailServices = emailServices;
            _customerRepository = CustomerRepository;
        }

        public string AddCustomer(Customer customer)
        {
            if (!customer.IsValid())
                return "Invalid data";

            _customerRepository.AddCustomer(customer);

            _emailServices.Send("contact@company.com", customer.Email, "Welcome", "Congratulations, you are registered");

            return "Customer successfully registered.";
        }
    }
}