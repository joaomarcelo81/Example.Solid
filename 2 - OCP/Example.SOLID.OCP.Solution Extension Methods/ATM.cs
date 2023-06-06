using System;

namespace Example.SOLID.OCP.Solution_Extension_Methods
{
    public class ATM
    {
        public static void Operations()
        {
            OperationsMenu();

            var option = Console.ReadKey();
            var response = string.Empty;

            var debitAccount = DebitData();

            switch (option.KeyChar)
            {
                case '1':
                    Console.WriteLine("Performing transactions in a Current Account.");
                    response = debitAccount.WithdrawAccount();
                    break;
                case '2':
                    Console.WriteLine("Performing transactions in a Savings Account.");
                    response = debitAccount.WithdrawSavingAccount();
                    break;
                case '3':
                    Console.WriteLine("Performing transactions in an Investment Account.");
                    response = debitAccount.WithdrawInvestmentAccount();
                    break;
            }

            TransactionResult(response);
        }

        private static void OperationsMenu()
        {
            Console.Clear();
            Console.WriteLine("SOLID ATM");
            Console.WriteLine("Choose your option:");
            Console.WriteLine();
            Console.WriteLine("1 - Current Account Withdrawal");
            Console.WriteLine("2 - Savings Account Withdrawal");
            Console.WriteLine("3 - Investment Account Withdrawal");

        }

        private static DebitAccount DebitData()
        {
            Console.WriteLine();
            Console.WriteLine("..............................");
            Console.WriteLine();
            Console.WriteLine("Enter the Account");
            var account = Console.ReadLine();
            Console.WriteLine("Enter the Amount");
            var amount = Convert.ToDecimal(Console.ReadLine());


            var debitAccount = new DebitAccount()
            {
                AccountNumber = account,
                Account = amount
            };

            return debitAccount;
        }

        private static void TransactionResult(string response)
        {
            Console.WriteLine();
            Console.WriteLine("Success! Transaction: " + response);
            Console.ReadKey();
        }
    }
}