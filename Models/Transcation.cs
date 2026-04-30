using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Transcation
    {
        public  string TranscationNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public  decimal Amount { get; set; }    
        public  string TranscationType { get; set; }    
        public  string Note { get; set; }

        public  string AccountNumber { get; set; }
        public  Account Account { get; set; }

    }
}
