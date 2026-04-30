using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class CustomerAccount
    {
        public  int Id { get; set; }
        public  string AccountStatus { get; set; }
        public  string OwnershipType { get; set; }
        public  DateTime OwnershipStartDate { get; set; }

        public  int CustomerId { get; set; }
        public  Customer Customer { get; set; } 
        public  string AccountNumber { get; set; }
        public  Account Account { get; set; }

    }
}
