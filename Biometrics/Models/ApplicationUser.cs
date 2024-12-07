using System;
using Microsoft.AspNetCore.Identity;

namespace Biometrics.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public string TestAdministeredBy { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public string Role { get; set; }
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}