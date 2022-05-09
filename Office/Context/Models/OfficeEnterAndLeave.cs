using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Context.Models
{
    public class OfficeEnterAndLeave
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public bool IsTypeEnter { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
