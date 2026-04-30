using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Account 
    {
        public string  AccountNumber  { get; set; }
        public  decimal CurrentBalance { get; set; }    

        public  string AccountType  { get; set; }

        public  DateTime OpeningDate { get; set; }
         public int BranchCode { get; set; }
         public Branch Branch { get; set; }

        public  ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();  
        public ICollection<Transcation> Transactions { get; set; } = new List<Transcation>();
    }
}
