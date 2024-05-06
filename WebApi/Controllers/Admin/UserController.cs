using Application.DTOs.Account;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            return Ok(await _accountService.RegisterAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _accountService.UserList());
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(string Id, UpdateUser model)
        {
            return Ok(await _accountService.UpdateUser(Id, model));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            return Ok(await _accountService.DeleteUser(Id));
        }
    }
}
