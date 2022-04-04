//using System;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using System.Linq;

//namespace Intex2.Models
//{
//    public static class IdentitySeedData
//    {
//        private const string adminUser = "Admin";
//        private const string adminPassword = "Admin123";

//        // method we are calling to make sure their is data in database to begin with 
//        public static async void EnsurePopulated(IApplicationBuilder app)
//        {
//            AppIdentityDBContext context = app.ApplicationServices
//                .CreateScope().ServiceProvider
//                .GetRequiredService<AppIdentityDBContext>();

//            // if there are any pending migrations then run them
//            if (context.Database.GetPendingMigrations().Any())
//            {
//                context.Database.Migrate();
//            }

//            UserManager<IdentityUser> userManager = app.ApplicationServices
//                .CreateScope().ServiceProvider
//                .GetRequiredService<UserManager<IdentityUser>>();

//            // if this user does not already exist then we are going to create another one
//            IdentityUser user = await userManager.FindByIdAsync(adminUser);

//            if (user == null)
//            {
//                user = new IdentityUser(adminUser);

//                user.Email = "admin@yeet.com";
//                user.PhoneNumber = "222-222-2222";

//                await userManager.CreateAsync(user, adminPassword);
//            }
//        }
//    }
//}