using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Context.Dtos
{
    public class OfficeEnterAndLeaveRequest
    {
        [Required]
        public int TagId { get; set; }
        //public bool IsTypeEnter { get; set; }
        public DateTime TimeChecked { get; set; }
    }
}
