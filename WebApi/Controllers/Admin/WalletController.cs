using Application.DTOs.Account;
using Application.DTOs.Funds;
using Application.Features.Funds.Commands.Create;
using Application.Interfaces;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Balance(AccountDetails request)
        {
            var createFunds = new CreateFunds()
            {
                UserId = request.UserId,
                Amount = request.Amount,
                WalletAccount = request.WalletAccount
            };

            return Ok(await Mediator.Send(createFunds));
        }
    }
}
