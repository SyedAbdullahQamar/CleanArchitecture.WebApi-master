using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFundsRepository : IGenericRepositoryAsync<UserAccount>
    {
        Task<UserAccount> GetByUserIdAsync(string senderId);
    }
}
