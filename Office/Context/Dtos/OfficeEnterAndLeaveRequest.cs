using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Context.Dtos
{
    public class OfficeEnterAndLeaveRequest
    {
        public int TagId { get; set; }
        //public bool IsTypeEnter { get; set; }
        public DateTime TimeChecked { get; set; }
    }
}
