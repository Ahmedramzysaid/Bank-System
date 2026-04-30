using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Manager
    {
        public  int  Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public  string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public  int BranchCode { get; set; }
        public  Branch Branch { get; set; }
    }
}
