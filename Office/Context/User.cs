using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Office.Context.Models;

namespace Office.Context
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User 
    {   [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }


    }
}
