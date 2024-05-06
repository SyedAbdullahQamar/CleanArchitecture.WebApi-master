using Application.DTOs.Funds;
using Application.Features.Funds.Commands.Create;
using Application.Features.Funds.Commands.Update;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class FundsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> TransferFunds(TransferFundRequest request)
        {
            var updateFunds = new UpdateFunds()
            {
                Amount = request.Amount,
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId
            };

            return Ok(await Mediator.Send(updateFunds));
        }
    }
}
