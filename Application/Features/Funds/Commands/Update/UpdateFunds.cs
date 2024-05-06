using Application.Interfaces.Repositories;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entities;

namespace Application.Features.Funds.Commands.Update
{
    public class UpdateFunds : IRequest<Response<string>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdateFundsHandler : IRequestHandler<UpdateFunds, Response<string>>
    {
        public IFundsRepository _repository { get; set; }
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public UpdateFundsHandler(IFundsRepository repository, IMapper mapper, IAccountService accountService)
        {
            _repository = repository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(UpdateFunds request, CancellationToken cancellationToken)
        {
            var senderDetail = await _repository.GetByUserIdAsync(request.SenderId);

            if(senderDetail == null)
            {
                throw new ApiException("Invalid sender.");
            }
            else
            {
                if (senderDetail.Amount >= request.Amount)
                {
                    var receiverDetail = await _repository.GetByUserIdAsync(request.ReceiverId);

                    if (receiverDetail != null)
                    {
                        receiverDetail.Amount = receiverDetail.Amount + request.Amount;
                        await _repository.UpdateAsync(receiverDetail);
                    }
                    else
                    {
                        var result = await _accountService.GetUserById(request.ReceiverId);

                        if (result != null)
                        {
                            var userAccount = new UserAccount()
                            {
                                Amount = request.Amount,
                                UserId = request.ReceiverId,
                                WalletAccount = result.PhoneNumber
                            };

                            var funds = _mapper.Map<UserAccount>(userAccount);
                            await _repository.AddAsync(funds);
                        }
                        else
                            throw new ApiException("Receiver not found.");
                    }

                    senderDetail.Amount = senderDetail.Amount - request.Amount;
                    await _repository.UpdateAsync(senderDetail);

                    return new Response<string>(data: null, "Funds transfered successfully.");
                }
                else
                {
                    throw new ApiException("Insufficient funds.");
                }
            }
        }
    }
}
