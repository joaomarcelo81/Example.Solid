using System.Diagnostics;
using System.Security.Principal;

namespace Example.SOLID.OCP.Violation
{
    public class DebitAccount
    {
        public void Debit(decimal value, string account, TypeAccount typeAccount)
        {
            if (typeAccount == TypeAccount.Account)
            {
                // Debit Account Account
            }

            if (typeAccount == TypeAccount.SavingsAccount)
            {
                // Validate Account Birthday
                // Debit Savings Account
            }
        }
    }
}