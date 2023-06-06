namespace Example.SOLID.DIP.Solution.Interfaces
{
    public interface IEmailServices
    {
        bool IsValid(string email);
        void Send(string from, string to, string subject, string message);
    }
}