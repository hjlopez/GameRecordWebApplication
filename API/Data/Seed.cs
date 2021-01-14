using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // insert admin on first run
    public class Seed
    {
        public static async Task AddGamesAndTypes(DataContext context)
        {
            if (await context.Games.AnyAsync()) return;

            // insert values
            context.GameTypes.Add(new GameTypes{Type = "Cue"});
            context.GameTypes.Add(new GameTypes{Type = "RPG"});
            context.GameTypes.Add(new GameTypes{Type = "MOBA"});

            await context.SaveChangesAsync();

            context.Games.Add(new Games{Name = "Billiards", GameTypesId = 1, LoggedInLink = "user/billiards", LoggedOutLink = "billiards"});
            context.Games.Add(new Games{Name = "Monster Hunter", GameTypesId = 1, LoggedInLink = "user/mh", LoggedOutLink = "mh"});
            context.Games.Add(new Games{Name = "DOTA 2", GameTypesId = 1, LoggedInLink = "user/dota", LoggedOutLink = "dota"});

            await context.SaveChangesAsync();
        }

        public static async Task SeedAdmins(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return; // if there is users, return 

            // insert list of roles
            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"}
            };

            // insert role in database
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "P@ssw0rdP@ssword");
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Member"});

        }
    }
}