using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BalekShop.Data
{
    public static class ApplicationDbInitializer
    {
        public static void Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            bool adminRoleReady = false;
            bool adminUserReady = false;

            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                var role = new IdentityRole()
                {
                    Name = "Admin",
                };

                var res = roleManager.CreateAsync(role).Result;

                adminRoleReady = res.Succeeded;
            }

        }

    }
}