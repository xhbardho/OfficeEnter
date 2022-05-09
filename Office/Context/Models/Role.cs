using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Office.Context.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedUser { get; set; }
        public List<User> Users { get; set; }


    }
}
