namespace Example.SOLID.OCP.Solution
{
    public class DebitSavingsAccount : DebitAccount
    {
        public override string Withdraw(decimal amount, string account)
        {
            // Validate Account Birthday
            // Debit Account Account
            return FormatTransaction();
        }
    }
}