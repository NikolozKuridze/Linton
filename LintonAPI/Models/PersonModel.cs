using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LintonAPI.Models
{
    public class PersonModel
    { 
        public string FullName { get; set; }
        public int Age { get; set; }
        public string IBAN { get; set; }
        public string BankName { get; set; }
    }
}
