namespace Example.SOLID.OCP.Solution
{
    public class DebitAccountAccount : DebitAccount
    {
        public override string Withdraw(decimal amount, string account)
        {
            // Debita Conta Account
            return FormatTransaction();
        }
    }
}