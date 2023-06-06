namespace Example.SOLID.ISP.Solution.Interfaces
{
    public interface ICustomerRegistration
    {
        void ValidateData();
        void SaveBank();
        void SendEmail();
    }
}