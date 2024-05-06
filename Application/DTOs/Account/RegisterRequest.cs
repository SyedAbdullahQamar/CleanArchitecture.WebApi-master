using Application.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression("^[0-9]*$")]
        public string PhoneNumber { get; set; }

        [Required]
        [DateOfBirth(MinAge = 0, MaxAge = 150)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
