using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Office.Context;
using Office.Context.Dtos;
using Office.Context.Models;
using Office.Controllers;
using Office.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Office.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly OfficeDbContext _officeDbContext;

        public UserService(ILogger<UserService> logger, OfficeDbContext officeDbContext)
        {
            _logger = logger;
            _officeDbContext = officeDbContext;
        }
        public  User AddUser(RegisterViewModel user)
        {
            try
            {
                User applicationUser = new User
                {
                    Username = user.Username,
                    Password = Helper.PasswordEncrypt.Encrypt(user.Password),
                };
                applicationUser.RoleId = GetRoleByName(user.RoleName).Id;
                _officeDbContext.Users.Add(applicationUser);
                _officeDbContext.SaveChanges();
                return applicationUser;
            }
            catch (System.Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public bool IsValidUser(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }
        public  Role GetRoleByName(string roleName)
        {
            try
            {
                var role = _officeDbContext.Roles.Where(x=>x.Name==roleName).FirstOrDefault();
                return role;
            }
            catch (System.Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public List<Role> GetRoles()
        {
            try
            {
                var roles = _officeDbContext.Roles.ToList(); ;
                return roles;
            }
            catch (System.Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public User Login(LoginRequest login)
        {
            User user = new User();
            try
            {
                login.Password = Helper.PasswordEncrypt.Encrypt(login.Password);
                 user = _officeDbContext.Users.Where(x => x.Username == login.UserName && x.Password ==login.Password)
                    .Include(x=>x.Role).FirstOrDefault()
                    ;
                return user;
            }
            catch (System.Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public List<UserViewModel> GetAllUsers()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            try
            {
               var usersList = _officeDbContext.Users.Include(x=>x.Role).ToList();
                foreach (var item in usersList)
                {
                    users.Add(new UserViewModel(item));
                }
            }
            catch (System.Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
            return users;
        }
        public bool DoesUserExists(string userName)
        {
            try
            {
                var users = _officeDbContext.Users.FirstOrDefault(x => x.Username == userName);
                if (users == null)
                    return false;
                else return true;
            }
            catch (System.Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
        public Tag GetTagForUser(int userId)
        {
            try
            {
                var tag = _officeDbContext.Tags.FirstOrDefault(x=>x.UserId==userId);
                return tag;
            }
            catch (System.Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Something wrong happend! Error: " + ex.Message),
                    ReasonPhrase = "Something wrong happend! Error: " + ex.Message
                };
                throw new System.Web.Http.HttpResponseException(response);
            }
        }
    }
}
