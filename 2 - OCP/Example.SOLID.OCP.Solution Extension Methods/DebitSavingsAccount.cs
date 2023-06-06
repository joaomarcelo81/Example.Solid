namespace Example.SOLID.OCP.Solution_Extension_Methods
{
    public static class DebitSavingsAccount
    {
        public static string WithdrawSavingAccount(this DebitAccount debitAccount)
        {
            // Business logic for withdrawal from a savings account.
            return debitAccount.FormatTransaction();
        }
    }
}