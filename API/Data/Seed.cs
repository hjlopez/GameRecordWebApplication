using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.PBA;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // insert admin on first run
    public class Seed
    {
        public static async Task SeedPBA(DataContext context)
        {
            await SeedTeams(context);
            await SeedSched(context);
            await SeedConference(context);
        }

        private static async Task SeedTeams(DataContext context)
        {
            if (await context.Teams.AnyAsync()) return;

            context.Teams.Add(
                new Team{TeamName = "Alaska Aces", TeamLogo = "https://dashboard.pba.ph/assets/logo/ALA_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Barangay Ginebra San Miguel", TeamLogo = "https://dashboard.pba.ph/assets/logo/GIN_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Blackwater Bossing", TeamLogo = "https://dashboard.pba.ph/assets/logo/Blackwater_new_logo_2021.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Northport Batang Pier", TeamLogo = "https://dashboard.pba.ph/assets/logo/GLO_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Terrafirma Dyip", TeamLogo = "https://dashboard.pba.ph/assets/logo/terrafirma.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Meralco Bolts", TeamLogo = "https://dashboard.pba.ph/assets/logo/MER_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "NLEX Road Warriors", TeamLogo = "https://dashboard.pba.ph/assets/logo/NLX_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Rain or Shine Elasto Painters", TeamLogo = "https://dashboard.pba.ph/assets/logo/ROS_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "San Miguel Beermen", TeamLogo = "https://dashboard.pba.ph/assets/logo/SMB2020_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "TNT Tropang Giga", TeamLogo = "https://dashboard.pba.ph/assets/logo/TNT_2020.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Magnolia Hotshots Pambansang Manok", TeamLogo = "https://dashboard.pba.ph/assets/logo/MAG_web.png"}
            );

            context.Teams.Add(
                new Team{TeamName = "Phoenix Super LPG Fuel Masters", TeamLogo = "https://dashboard.pba.ph/assets/logo/phx2020.png"}
            );
            
            await context.SaveChangesAsync();
        }

        private static async Task SeedSched(DataContext context)
        {
            if (await context.ScheduleGroups.AnyAsync()) return;

            context.ScheduleGroups.Add(new ScheduleGroup{GroupName = "Default", IsDefault = true});

            await context.SaveChangesAsync();
        }

        private static async Task SeedConference(DataContext context)
        {
            if (await context.Conferences.AnyAsync()) return;

            context.Conferences.Add(new Conference{ConferenceName = "First Conference"});
            context.Conferences.Add(new Conference{ConferenceName = "Second Conference"});
            context.Conferences.Add(new Conference{ConferenceName = "Third Conference"});

            await context.SaveChangesAsync();
        }


        public static async Task AddGamesAndTypes(DataContext context)
        {
            if (await context.Games.AnyAsync()) return;

            // insert values
            // context.GameTypes.Add(new GameTypes{Type = "Cue"});
            // context.GameTypes.Add(new GameTypes{Type = "RPG"});
            // context.GameTypes.Add(new GameTypes{Type = "MOBA"});

            // await context.SaveChangesAsync();

            context.Games.Add(new Games{Name = "Monster Hunter", GameTypesId = 1, LoggedInLink = "user/mh", LoggedOutLink = "mh"});
            context.Games.Add(new Games{Name = "Billiards", GameTypesId = 1, LoggedInLink = "user/billiards", LoggedOutLink = "billiards"});
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