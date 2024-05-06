using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Funds.Commands.Create
{
    public class CreateFunds : IRequest<Response<string>>
    {
        public decimal Amount { get; set; } = 0;
        public string UserId { get; set; }
        public string WalletAccount { get; set; }
    }

    public class CreateFundsHandler : IRequestHandler<CreateFunds, Response<string>>
    {
        public IFundsRepository _repository { get; set; }
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public CreateFundsHandler(IFundsRepository repository, IMapper mapper, IAccountService accountService)
        {
            _repository = repository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<Response<string>> Handle(CreateFunds request, CancellationToken cancellationToken)
        {
            var detail = await _repository.GetByUserIdAsync(request.UserId);

            if (detail != null)
            {
                detail.Amount = detail.Amount + request.Amount;
                await _repository.UpdateAsync(detail);
            }
            else
            {
                var result = await _accountService.GetUserById(request.UserId);

                if (result != null)
                {
                    var userAccount = new UserAccount()
                    {
                        Amount = request.Amount,
                        UserId = request.UserId,
                        WalletAccount = result.PhoneNumber
                    };

                    var funds = _mapper.Map<UserAccount>(userAccount);
                    await _repository.AddAsync(funds);
                }
                else
                    throw new ApiException("no user found.");                
            }

            return new Response<string>(data: null, "Balance added successfully.");
        }
    }
}
