using Office.Context.Models;
using System.ComponentModel.DataAnnotations;

namespace Office.Context.Dtos
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public UserViewModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
            RoleId = user.RoleId;
            RoleName = user.Role.Name;
        }
    }
}
