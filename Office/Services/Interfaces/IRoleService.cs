using Office.Context;
using Office.Context.Dtos;
using Office.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Services.Interfaces
{
    public interface IRoleService
    {

        Task<Role> GetRoleByName(string roleName);

    }
}
