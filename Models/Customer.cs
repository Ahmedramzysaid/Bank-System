using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public  class Customer
    {
         public  int Id { get; set; }   
         public  string FullName { get; set; }
         public  string Email { get; set; }
         public  string PhoneNumber { get; set; }    
         public  DateTime DateOfBirth { get; set; }
         public  string Address { get; set; }
         public  string NationalId { get; set; }
         public  string CustomerType { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; } = new List<CustomerAccount>();
    }
}
