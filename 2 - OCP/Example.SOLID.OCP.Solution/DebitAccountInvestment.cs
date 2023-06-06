namespace Example.SOLID.OCP.Solution
{
    public class DebitAccountInvestment : DebitAccount
    {
        public override string Withdraw(decimal amount, string account)
        {
            // Debita Conta Investimento
            // Isentar Taxas
            return FormatTransaction();
        }
    }
}