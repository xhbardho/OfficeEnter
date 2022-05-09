using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Context.Dtos
{
    public class LoginResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>admin</example>
        public string UserName { get; set; }
        public string JwtToken { get; set; }
    }
}
