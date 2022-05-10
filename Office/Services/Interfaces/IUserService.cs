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
        bool DoesUserExists(string userName);
        User AddUser(RegisterViewModel user);
        Role GetRoleByName(string roleName);
        User Login(LoginRequest login);
        List<UserViewModel> GetAllUsers();
        List<Role> GetRoles();
        Tag GetTagForUser(int userId);
    }
}
