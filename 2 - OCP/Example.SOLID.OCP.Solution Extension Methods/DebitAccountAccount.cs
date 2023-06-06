namespace Example.SOLID.OCP.Solution_Extension_Methods
{
    public static class DebitAccountAccount
    {
        public static string WithdrawAccount(this DebitAccount debitAccount)
        {
            // Business logic for withdrawing funds from a checking account.
            return debitAccount.FormatTransaction();
        }
    }
}