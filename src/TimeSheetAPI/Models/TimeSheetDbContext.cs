using Microsoft.EntityFrameworkCore;

namespace TimeSheetAPI.Models
{
    public class TimeSheetDbContext : DbContext
    {
        public TimeSheetDbContext(DbContextOptions<TimeSheetDbContext> options)
            : base(options)
        { }
    }
}