using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Context.Models
{
    public class TagStatus
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<Tag> tags { get; set; }
    }
}
