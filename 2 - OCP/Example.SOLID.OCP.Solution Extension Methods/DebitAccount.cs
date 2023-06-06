﻿using System;
using System.Linq;

namespace Example.SOLID.OCP.Solution_Extension_Methods
{
    public class DebitAccount
    {
        public string AccountNumber { get; set; }
        public decimal Account { get; set; }
        public string TransactionNumber { get; set; }
        
        public string FormatTransaction()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            TransactionNumber = new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            // Formatted transaction number
            return TransactionNumber;
        } 
    }
}