using Microsoft.EntityFrameworkCore;

namespace TimeSheetAPI.Models
{
    public class TimeSheetDbContext : DbContext
    {
        public DbSet<UserLogin> UserLogins { set; get; }

        public TimeSheetDbContext(DbContextOptions<TimeSheetDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>()
                .HasKey(c => c.Id)
                .HasName("PrimaryKey_UserLoginId");
        }
    }
}