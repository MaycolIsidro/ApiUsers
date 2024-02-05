using API_Users.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Users
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users").HasKey(p => p.IdUser);
        }
        public DbSet<User> Users { get; set; }
    }
}
