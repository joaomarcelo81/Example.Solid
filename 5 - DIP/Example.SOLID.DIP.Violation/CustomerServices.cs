namespace Example.SOLID.DIP.Violation
{
    public class CustomerServices
    {
        public string AddCustomer(Customer customer)
        {
            if (!customer.IsValid())
                return "Invalid data";

            var repo = new CustomerRepositor();
            repo.AddCustomer(customer);

            EmailServices.Send("contact@company.com", customer.Email, "Welcome", "Congratulations, you are registered");
            return "Customer successfully registered.";
        }
    }
}