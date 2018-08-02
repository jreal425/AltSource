using Banking.API.Domain;
using Banking.API.Models.Account;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.API.Services
{
    public class TransactionService
    {
        public TransactionService()
        {
        }

        public TransactionItem ProcessTransaction(TransactionRequest request, decimal currentBalance)
        {
            var transaction = new TransactionItem
            {
                Amount = request.Amount,
                Date = DateTime.Now,
                Balance = currentBalance,
                Type = request.Type
            };

            if (request.Type == TransactionType.Deposit)
                transaction.Balance += request.Amount;
            else
                transaction.Balance -= request.Amount;

            return transaction;
        }
    }
}
