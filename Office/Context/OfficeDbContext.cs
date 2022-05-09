using Microsoft.EntityFrameworkCore;
using Office.Context.Models;


namespace Office.Context
{
    public class OfficeDbContext: DbContext
    {
        public OfficeDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Office.Context.Models.Office> Offices { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<TagStatus> TagStatus { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<OfficeEnterAndLeave>  officeEnterAndLeaves { get; set; }



    }
}
