using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class GetTransactionDto
    {
        public string Date { get;  set; }
        public decimal Amount { get;  set; }
        public string CPF { get;  set; }
        public string CardNumber { get;  set; }
        public TimeSpan Hour { get;  set; }
        public string StoreOwner { get;  set; }
        public string StoreName { get;  set; }

        public virtual GetTransactionTypeDto TransactionType { get; set; }
    }    
}
