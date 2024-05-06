using Microsoft.AspNetCore.Identity;
using System;

namespace Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string JwtToken { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
