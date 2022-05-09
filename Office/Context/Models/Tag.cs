using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Context.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int IsValid { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TagStatusId { get; set; }
        public TagStatus TagStatus { get; set; }
        public IEnumerable<OfficeEnterAndLeave> officeEnterAndLeaves { get; set; }
    }
}
