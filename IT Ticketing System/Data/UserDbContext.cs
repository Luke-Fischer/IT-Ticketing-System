using IT_Ticketing_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace IT_Ticketing_System.Data
{
    public class UserDbContext :DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
