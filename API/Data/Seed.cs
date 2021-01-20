using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.MH;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // insert admin on first run
    public class Seed
    {
        public static async Task InsertWeaponTypes(DataContext context)
        {
            if (await context.WeaponTypes.AnyAsync()) return;

            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Great Sword",
                FirstIntroduced = "MH",
                Description = "Great Swords, also known as GS are large, slow weapons that were first introduced in Monster Hunter. "+
                            "Originally they had three main attacks: the sideswipe, the upswing, and the downstrike." + 
                            " The great sword has a long reach and powerful attacks. Because its sheer weight limits the speed "+
                            "of your movement and attacks, it lends itself well to hit-and-run combat tactics.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-greatsword-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Long Sword",
                FirstIntroduced = "MHF2",
                Description = "Long Swords , often referred to simply as LS, are long, "+
                        "slender blades designed for elegant movements. They were first introduced in Monster Hunter 2 "+
                        "(In the west, it was introduced in Monster Hunter Freedom 2). The long sword is a"+
                        "A nimble weapon capable of extended combos.Charge up energy with each attack to use your powerful Spirit Blade."+
                        " It enables fast, fluid movement and combos, but it cannot be used to guard.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-longsword-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Sword and Shield",
                FirstIntroduced = "MH",
                Description = "The Sword and Shield, also known as SnS, were first introduced in Monster Hunter. "+
                            "These weapons are small and accompanied by a shield that allows the Hunter to block most attacks. "+
                            "Although they deal relatively low damage in one strike, they are very quick, and it is possible to perform fairly long combos",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-sword-shield-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Dual Blades",
                FirstIntroduced = "MH",
                Description = "Dual Blades, also known as DB (formally known as Dual Swords and 'DS'), are similar to Sword and Shields "+
                            "but with a stronger focus on offense (at the expense of defense). Like Sword and Shields, individual"+
                            " Dual Blade attacks cause a small amount of damage, but they are fast and flow easily into combos.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-dual-blades-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Hammer",
                FirstIntroduced = "MH",
                Description = "Hammers are large, heavy weapons which are capable of dealing the vast amounts of damage in a single combo."+
                                "Their tremendous size only slightly hinders mobility, and Hunters are still able to run with them,"+
                                " unlike with the Great Sword and Lance",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-hammer-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Hunting Horn",
                FirstIntroduced = "MHF2",
                Description = "Hunting Horns, also known as HH, are similar to the Hammer in that they do Impact "+
                            "damage and able to K.O a monster when they connect with the head. " +
                            "The hunting horn is a blunt weapon that can bestow positive status effects on yourself and your allies."+
                            " Use it to perform melodies that can boost attack power, restore health, and grant other beneficial effects.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-hunting-horn-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Lance",
                FirstIntroduced = "MH",
                Description = "Lances are long weapons that can strike from a distance. The Lance is always "+
                            "accompanied by a large shield which grants a powerful defense against most attacks. "+
                            "Although slow and difficult to travel with, the weapon's damage output can be considerable.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-lance-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Gunlance",
                FirstIntroduced = "MHF2",
                Description = "Gunlances, also known as GL, are long, piercing weapons with a mechanism inside which can fire an explosive round"+
                            "Its distinctive feature is its ability to shoot blank shells that "+
                            "explode in front of the hunter, dealing damage to monsters caught in the explosion.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-gunlance-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Switch Axe",
                FirstIntroduced = "MH3",
                Description = "Switch Axes, also known as SA, are weapons introduced in Monster Hunter 3. There are two modes:"+
                            " Axe and Sword. It is possible to transform between the two near-instantly. "+
                            "While the weapon is drawn, the running speed in Axe-mode is similar to that of a Long Sword; "+
                            "however, Sword-mode's running speed is similar to the Great Sword.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-switch-axe-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Insect Glaive",
                FirstIntroduced = "MH4",
                Description = "Insect Glaives, also known as IG, are weapons introduced in Monster Hunter 4. "+
                            "This double-ended rod is capable of quick, fluid attacks (like the Long Sword) and "+
                            "allows the hunter to jump at any time in a pole-vaulting fashion. It also is capable "+
                            "of summoning a Kinsect and using it to attack monsters.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-insect-glaive-mhw_tree_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Melee",
                Weapon = "Charge Blade",
                FirstIntroduced = "MH4",
                Description = "Charge Blades, also known as CB, are weapons introduced in Monster Hunter 4. "+
                            "It is similar to the Switch Axe with its ability to switch between two different weapon modes. "+
                            "While in Sword Mode, hunters will be able to perform fast combo attacks with a sword and guard with a"+
                            " shield, making it very similar to the Sword and Shield class. The Axe Mode boasts superior reach and power with its strikes. ",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-charge-blade-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Range",
                Weapon = "Light Bowgun",
                FirstIntroduced = "MH",
                Description = "Light Bowguns, also known simply as LBG, are ranged weapons. As the name suggests, they are "+
                            "light enough to allow Hunters to run with the weapon drawn, and they can be sheathed and unsheathed very quickly."+
                            "It utilizes a variety of ammo to do everything from rapidly shooting targets to providing support with status-changing ammo.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-light-bowgun-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Range",
                Weapon = "Heavy Bowgun",
                FirstIntroduced = "MH",
                Description = "Heavy Bowguns, also known as HBG, are ranged weapons that deal considerable damage, but due to their size "+
                            "and weight, do not allow Hunters to run when drawn. Heavy Bowguns are able to use a wide range of Ammo."+
                            "It is the artillery of ranged weapons. It specializes in high damage rounds at a range.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-heavy-bowgun-mhw_tree.png"
            });
            context.WeaponTypes.Add(new WeaponType{
                WeaponClass = "Range",
                Weapon = "Bow",
                FirstIntroduced = "MHF2",
                Description = "The Bow is a fast, unique ranged weapon. An infinite quiver of arrows is automatically provided, "+
                            "although each individual doesn't do a great deal of damage. "+
                            "It has a variety of shots, including the long-ranged Arc Shot and the damaging Power Shot, "+
                            "and allows its user to be highly mobile as they fire off coated arrows as support.",
                IconUrl = "https://monsterhunterworld.wiki.fextralife.com/file/Monster-Hunter-World/rare-1-bow-mhw_tree.png"
            });

            await context.SaveChangesAsync();
        }
        public static async Task InsertMHGames(DataContext context)
        {
            if (await context.MHGames.AnyAsync()) return;

            context.MHGames.Add(new MHGames{
                GenerationNumber = 1,
                GameName = "Monster Hunter",
                ShortName = "MH",
                JapaneseName = "Monster Hunter",
                JapShortName = "MH",
                FlagshipMonster = "Rathalos",
                InitialYearRelease = 2004,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/c/c9/Logo-MH1.png/revision/latest/scale-to-width-down/300?cb=20140731093534",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/8/8c/MHW-Rathalos_Icon.png/revision/latest/scale-to-width-down/475?cb=20181008232240"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 1,
                GameName = "Monster Hunter Freedom",
                ShortName = "MHF",
                JapaneseName = "Monster Hunter Portable",
                JapShortName = "MHP",
                FlagshipMonster = "Rathalos",
                InitialYearRelease = 2005,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/a/a5/Logo-MHF1.png/revision/latest/scale-to-width-down/200?cb=20100102083225",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/8/8c/MHW-Rathalos_Icon.png/revision/latest/scale-to-width-down/475?cb=20181008232240"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 2,
                GameName = "Monster Hunter Freedom 2",
                ShortName = "MHF2",
                JapaneseName = "Monster Hunter Portable 2nd",
                JapShortName = "MHP2",
                FlagshipMonster = "Tigrex",
                InitialYearRelease = 2007,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/2/29/Logo-MHF2.png/revision/latest/scale-to-width-down/150?cb=20100102083216",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/8/8b/MHWI-Tigrex_Icon.png/revision/latest/scale-to-width-down/61?cb=20190704205459"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 2,
                GameName = "Monster Hunter Freedom Unite",
                ShortName = "MHFU",
                JapaneseName = "Monster Hunter Portable 2nd G",
                JapShortName = "MHP2G",
                FlagshipMonster = "Nargacuga",
                InitialYearRelease = 2008,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/9/92/Logo-MHFU.png/revision/latest/scale-to-width-down/250?cb=20100101125755",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/b/ba/MHWI-Nargacuga_Icon.png/revision/latest/scale-to-width-down/62?cb=20200825145336"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 3,
                GameName = "Monster Hunter 3",
                ShortName = "MH3",
                JapaneseName = "Monster Hunter Tri",
                JapShortName = "MH3",
                FlagshipMonster = "Lagiacrus",
                InitialYearRelease = 2009,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/8/86/Logo-MH3_EN.png/revision/latest/scale-to-width-down/150?cb=20100130073006",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/6/69/MH10th-Lagiacrus_Icon.png/revision/latest/scale-to-width-down/132?cb=20140805045923"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 3,
                GameName = "Monster Hunter 3 Ultimate",
                ShortName = "MH3U",
                JapaneseName = "Monster Hunter 3 G",
                JapShortName = "MH3G",
                FlagshipMonster = "Brachydios",
                InitialYearRelease = 2011,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/8/8b/Logo-MH3U.png/revision/latest/scale-to-width-down/150?cb=20120914171355",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/6/66/MHWI-Brachydios_Icon.png/revision/latest/scale-to-width-down/62?cb=20190908022508"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 4,
                GameName = "Monster Hunter 4 Ultimate",
                ShortName = "MH4U",
                JapaneseName = "Monster Hunter 4 G",
                JapShortName = "MH4G",
                FlagshipMonster = "Seregios",
                InitialYearRelease = 2014,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/1/1f/Logo-MH4U.png/revision/latest/scale-to-width-down/150?cb=20140610183737",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/6/66/MH4U-Seregios_Icon.png/revision/latest/scale-to-width-down/62?cb=20161212031455"                
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 4,
                GameName = "Monster Hunter Generations",
                ShortName = "MHG",
                JapaneseName = "Monster Hunter X",
                JapShortName = "MHX",
                FlagshipMonster = "Glavenus",
                InitialYearRelease = 2015,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/e/e2/Logo-MHGen.png/revision/latest/scale-to-width-down/150?cb=20160304000212",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/8/87/MHWI-Glavenus_Icon.png/revision/latest/scale-to-width-down/62?cb=20190802225011"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 4,
                GameName = "Monster Hunter Generations Ultimate",
                ShortName = "MHGU",
                JapaneseName = "Monster Hunter XX",
                JapShortName = "MHXX",
                FlagshipMonster = "Valstrax",
                InitialYearRelease = 2017,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/3/31/Logo-MHXX.png/revision/latest/scale-to-width-down/150?cb=20161027123645",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/9/95/MHGU-Valstrax_Icon.png/revision/latest/scale-to-width-down/62?cb=20170317181136"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 5,
                GameName = "Monster Hunter World",
                ShortName = "MHW",
                JapaneseName = "Monster Hunter World",
                JapShortName = "MHW",
                FlagshipMonster = "Nergigante",
                InitialYearRelease = 2018,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/4/42/Logo-MHW.png/revision/latest/scale-to-width-down/150?cb=20170613042934",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/4/4c/MHW-Nergigante_Icon.png/revision/latest/scale-to-width-down/61?cb=20201125145713"
            });
            context.MHGames.Add(new MHGames{
                GenerationNumber = 5,
                GameName = "Monster Hunter World Iceborne",
                ShortName = "MHWI",
                JapaneseName = "Monster Hunter World Iceborne",
                JapShortName = "MHWI",
                FlagshipMonster = "Velkhana",
                InitialYearRelease = 2019,
                IconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/e/e1/Logo-MHWI.png/revision/latest?cb=20181212004914",
                FlagshipIconUrl = "https://static.wikia.nocookie.net/monsterhunter/images/8/85/MHWI-Velkhana_Icon.png/revision/latest/scale-to-width-down/62?cb=20190802234311"
            });

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