using Microsoft.EntityFrameworkCore;
using EvonaZadatak.Models;

namespace EvonaZadatak.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Award> Awards { get; set; }

        public DbSet<AwardUser> Awards_Users { get; set; }
    }
}