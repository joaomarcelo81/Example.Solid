namespace Example.SOLID.ISP.Violation
{
    public interface IRegistration
    {
        void ValidateData();
        void SaveBank();
        void SendEmail();
    }
}