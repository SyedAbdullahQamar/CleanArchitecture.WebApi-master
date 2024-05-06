using Application.DTOs.Account;
using Application.Enums;
using Domain.Settings;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Abdullah",
                LastName = "Qamar",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                DateOfBirth = DateTime.Now,
                PhoneNumber = string.Empty
            };

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(defaultUser, userManager, configuration);
            AuthenticationResponse response = new AuthenticationResponse();
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            defaultUser.JwtToken = jwtToken;
            defaultUser.ExpiresOn = jwtSecurityToken.ValidTo;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "P@ssw0rd");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
            }
        }

        private static async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSettings:Key").Value));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: configuration.GetSection("JWTSettings:Issuer").Value,
                audience: configuration.GetSection("JWTSettings:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration.GetSection("JWTSettings:DurationInMinutes").Value)),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
