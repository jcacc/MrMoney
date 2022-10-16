using MrMoney.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrMoney.Domain.Entities
{
    public class Transaction
    {
        public long transactionID { get; set; }
        public long accountID { get; set; }
        public DateTime transactionDate { get; set; }
        public TransactionType transactionType  { get; set; }
        public string description { get; set; }
        public decimal transactionAmount { get; set; }
    }
}
