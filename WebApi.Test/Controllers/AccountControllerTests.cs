using Application.DTOs.Account;
using Application.Interfaces;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Controllers;

namespace WebApi.Test.Controllers
{
    public class AccountControllerTests
    {

        [Fact]
        public async Task TestExampleWithCorrectValues()
        {
            AuthenticationRequest request = new AuthenticationRequest()
            {
                Email = "superadmin@gmail.com",
                Password = "P@ssw0rd"
            };

            //Arrange

            //Act
            
            //Assert
        }
    }
}
