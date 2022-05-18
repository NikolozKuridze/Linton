using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LintonAPI.Helper
{
    public class Helper
    {
        public string GetBankName(string IBAN)
        {
            var bank = IBAN.Substring(4, 2).ToUpper();
            if (bank == "TB")
            {
                return "TBC BANK";
            }
            if (bank == "BG")
            {
                return "BANK OF GEORGIA";
            }
            return "Not Founded"; 
        }

        // აიბანის ვალიდურობის შემოწმება
        public bool checker(string iban)
        {
            if (!string.IsNullOrEmpty(iban))
            {
                Regex reg = new Regex(@"^[a-zA-Z]{2}[0-9]{2}\s?[a-zA-Z0-9]{4}\s?[0-9]{4}\s?[0-9]{3}([a-zA-Z0-9]\s?[a-zA-Z0-9]{0,4}\s?[a-zA-Z0-9]{0,4}\s?[a-zA-Z0-9]{0,4}\s?[a-zA-Z0-9]{0,3})?$");
                if (reg.IsMatch(iban))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
