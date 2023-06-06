using System;
using System.Linq;

namespace Example.SOLID.OCP.Solution
{
    public abstract class DebitAccount
    {
        public string TransactionNumber { get; set; }
        public abstract string Withdraw(decimal amount, string account);

        public string FormatTransaction()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            TransactionNumber = new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            // Formatted transaction number.
            return TransactionNumber;
        } 
    }
}