using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Infrastructure.Data;

namespace ContractingAuction.Infrastructure.Repositories;

public class AuctionRepository : BaseRepository<Auction>, IAuctionRepository
{
    public AuctionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}