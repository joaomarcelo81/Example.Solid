namespace Example.SOLID.OCP.Solution_Extension_Methods
{
    public static class DebitAccountInvestment
    {
        public static string WithdrawInvestmentAccount(this DebitAccount debitAccount)
        {
            // Business logic for withdrawing from an investment account.
            return debitAccount.FormatTransaction();
        }
    }
}