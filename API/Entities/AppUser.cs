using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        //public long Id { get; set; }
        //public string UserName { get; set; }
        public string GamerTag { get; set; }
        public bool PlayMH { get; set; } = false;
        public bool PlayDota { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Photo> Photos { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}