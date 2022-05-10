using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Office.Context.Dtos
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
