using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationApi.Domain.Entities
{
    public class AppUser
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string TelephoneNumber { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
    }
}
