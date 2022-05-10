using Microsoft.Extensions.Logging;
using Office.Context;
using Office.Context.Dtos;
using Office.Services.Interfaces;
using System.Threading.Tasks;
using Office.Context.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net;

namespace Office.Services
{


    public class RoleService : IRoleService
    {
        private readonly ILogger<UserService> _logger;
        private readonly OfficeDbContext _officeDbContext;
        public RoleService(ILogger<UserService> logger, OfficeDbContext officeDbContext)
        {
            _logger = logger;
            _officeDbContext = officeDbContext;
        }
        public async Task<Role> GetRoleByName(string roleName)
        {
            try
            {
                var role = (await _officeDbContext.Roles.Where(x => x.Name == roleName).FirstOrDefaultAsync());
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
    }
}
