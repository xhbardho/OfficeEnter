using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Office.Context.Dtos
{
    public class LoginRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>admin</example>
        [Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <example>securePassword</example>
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
