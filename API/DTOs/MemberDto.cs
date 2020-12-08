using System;

namespace API.DTOs
{
    public class MemberDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string GamerTag { get; set; }
        public string PhotoUrl { get; set; }
        public bool PlayMH { get; set; }
        public bool PlayDota { get; set; }
        public DateTime DateCreated { get; set; }
    }
}