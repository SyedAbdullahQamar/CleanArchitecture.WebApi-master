using Application.Features.Funds.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateFunds, UserAccount>().ReverseMap();
        }
    }
}
