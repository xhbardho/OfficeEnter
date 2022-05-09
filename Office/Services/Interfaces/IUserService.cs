using Office.Context;
using Office.Context.Dtos;
using Office.Context.Models;
using Office.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Services.Interfaces
{
    public interface IUserService
    {
        bool IsValidUser(string userName, string password);
        User AddUser(RegisterViewModel user);
        Role GetRoleByName(string roleName);

        User Login(LoginRequest login);
        List<User> GetAllUsers();

    }
}
