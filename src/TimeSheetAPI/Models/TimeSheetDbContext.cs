using Microsoft.EntityFrameworkCore;

namespace TimeSheetAPI.Models
{
    public class TimeSheetDbContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { set; get; }

        public DbSet<UserLogout> UserLogouts { set; get; }

        public TimeSheetDbContext(DbContextOptions<TimeSheetDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>()
                .HasKey(c => c.Id)
                .HasName("PrimaryKey_UserLoginId");
            
            modelBuilder.Entity<UserLogout>()
                .HasKey(c => c.Id)
                .HasName("PrimaryKey_UserLogoutId");
        }
    }
}