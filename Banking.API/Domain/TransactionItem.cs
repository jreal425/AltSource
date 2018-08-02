using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.API.Domain
{
    public class TransactionItem
    {
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public string DateFormatted => Date.ToString("MM/dd/yyyy");
    }
}
