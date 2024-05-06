using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class FundsRepository : GenericRepositoryAsync<UserAccount>, IFundsRepository
    {
        private readonly DbSet<UserAccount> _userAccounts;

        public FundsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userAccounts = dbContext.Set<UserAccount>();
        }

        public async Task<UserAccount> GetByUserIdAsync(string userId)
        {
            return await _userAccounts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
