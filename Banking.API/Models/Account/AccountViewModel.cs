using Banking.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.API.Models.Account
{
    public class AccountViewModel
    {
        public User User { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionItem> History { get; set; }

        public AccountViewModel()
        {
            History = new List<TransactionItem>();
        }
    }
}
