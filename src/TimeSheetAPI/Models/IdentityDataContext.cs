using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TimeSheetAPI.Models
{
    public class IdentityDataContext : IdentityDbContext<MyIdentityUser>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options) { }
    }
}