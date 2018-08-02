using Banking.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.API.Models.Account
{
    public class TransactionRequest
    {
        public string Email { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
