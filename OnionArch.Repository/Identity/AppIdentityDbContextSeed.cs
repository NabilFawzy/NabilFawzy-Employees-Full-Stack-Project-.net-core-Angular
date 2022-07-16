using Microsoft.AspNetCore.Identity;
using OnionArch.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArch.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    UserName = "nabil@gmail.com",
                    Email = "nabil@gmail.com",
                    NormalizedEmail = "nabil@gmail.com",
                    NormalizedUserName = "nabil@gmail.com",
                };
              var C=  await userManager.CreateAsync(user,"Pa$$w0rd");

               
            }

        }
    }
}
