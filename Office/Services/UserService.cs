using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Office.Context;
using Office.Context.Dtos;
using Office.Context.Models;
using Office.Controllers;
using Office.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Office.Services
{


    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly OfficeDbContext _officeDbContext;

        // inject database for user validation
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

            }
            throw new System.NotImplementedException();

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
                //var role = await _officeDbContext.Roles.Where(x => x.Name == roleName).FirstOrDefaultAsync();
                return role;
            }
            catch (System.Exception ex)
            {

                throw;
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

                throw;
            }
        }
    }
}
