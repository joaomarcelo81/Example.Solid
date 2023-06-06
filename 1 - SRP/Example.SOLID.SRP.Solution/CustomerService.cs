namespace Example.SOLID.SRP.Solution
{
    public class CustomerService
    {
        public string AddCustomer(Customer customer)
        {
            if (!customer.IsValid())
                return "Invalid data.";

            var repo = new CustomerRepository();
            repo.AddCustomer(customer);

            EmailServices.Send("contact@company.com", customer.Email, "Welcome", "Congratulations, you are registered.");

            return "Customer successfully registered.";
        }
    }
}