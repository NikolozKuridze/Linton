using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linton.Domain
{
    enum RecirdStatusType : byte
    {
        [DescriptionAttribute("აქტიური")]
        Active = 0,
        [DescriptionAttribute("წაშლილი")]
        Deleted = 1
    }
}
