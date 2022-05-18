using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Domain
{
    enum Banks : byte
    {
        [DescriptionAttribute("TBC BANK")]
        TB = 0,
        [DescriptionAttribute("BANK OF GEORGIA")]
        BG = 1
    } 
}
