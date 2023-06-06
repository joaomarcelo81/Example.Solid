namespace Example.SOLID.SRP.Solution
{
    public static class IdentificationServices
    {
        public static bool IsValid(string identification)
        {
            return identification.Length == 11;
        }
    }
}