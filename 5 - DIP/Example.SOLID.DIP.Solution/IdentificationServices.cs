using Example.SOLID.DIP.Solution.Interfaces;

namespace Example.SOLID.DIP.Solution
{
    public class IdentificationServices : IIdentificationServices
    {
        public bool IsValid(string identification)
        {
            return identification.Length == 11;
        }
    }
}