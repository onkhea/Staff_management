
using Microsoft.EntityFrameworkCore;
using StaffManagementAPI.Models;

namespace Staff_Management.Data
{
    public class StaffContext : DbContext
    {
        public StaffContext(DbContextOptions<StaffContext> options)
            : base(options)
        {
        }

        public DbSet<Staff> Staffs { get; set; }
    }
}

//This class handles the connection to the database and manages the entities.