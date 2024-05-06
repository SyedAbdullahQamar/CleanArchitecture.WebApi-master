using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string Email { get; set; }
        public string JWToken { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
