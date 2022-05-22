using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using TicketSystemApi.Models.ApplicationContext;

namespace TicketSystemApi.Models.DataSeeding
{
    public class SampleData
    {
        private readonly ApplicationDbContext _db;

        public SampleData(ApplicationDbContext db)
        {
            _db = db;
        }

        public void SeedAdminUser()
        {
            var user = new User
            {
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "Email@email.com",
                NormalizedEmail = "email@email.com",
                PhoneNumber = "01000000000",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(_db);

            if (!_db.Roles.Any(r => r.Name == "admin"))
            {
                roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" }).GetAwaiter().GetResult(); ;
            }

            if (!_db.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<User>(_db);
                userStore.CreateAsync(user).GetAwaiter().GetResult(); ;
                userStore.AddToRoleAsync(user, "admin").GetAwaiter().GetResult(); ;
                if (!_db.Roles.Any(r => r.Name == "user"))
                {
                    roleStore.CreateAsync(new IdentityRole { Name = "user", NormalizedName = "user" });
                }
                for (int i = 1; i <= 9; i++)
                {
                    user = new User
                    {
                        UserName = $"User{i}",
                        NormalizedUserName = $"User{i}",
                        Email = $"Email{i}@email.com",
                        NormalizedEmail = $"email{i}@email.com",
                        PhoneNumber = $"0100000000{i}",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                     password = new PasswordHasher<User>();
                     hashed = password.HashPassword(user, "password");
                    user.PasswordHash = hashed;
                    userStore.CreateAsync(user).GetAwaiter().GetResult(); ;
                    userStore.AddToRoleAsync(user, "user").GetAwaiter().GetResult(); ;
                }
            }

            _db.SaveChangesAsync();
        }

        public void ListOFUsers()
        {
            var roleStore = new RoleStore<IdentityRole>(_db);



        }
    }
}
