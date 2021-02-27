using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TimeSheetAPI.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "admin@gmail.com";

        private const string adminPassword = "Admin@123";

        public static async Task SeedDatabase(IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.CreateScope().ServiceProvider;
            var dbCtx = provider.GetRequiredService<IdentityDataContext>();
            dbCtx.Database.Migrate();  // do pending migrations

            var userManager = provider.GetRequiredService<UserManager<MyIdentityUser>>();
            var user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new MyIdentityUser(adminUser);
                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot create user: "
                        + result.Errors.FirstOrDefault());
                }
            }

            var testUser = await userManager.FindByNameAsync("test@gmail.com");
            if (testUser != null)
            {
                dbCtx.Users.Remove(testUser);
                await dbCtx.SaveChangesAsync();
            }
        }
    }
}
