using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContractingAuction.Infrastructure.Repositories;

public class BidRepository : BaseRepository<Bid>, IBidRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BidRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetUserTotalBids(string userId, int auctionId)
    {
        return await _dbContext.Bids.Where(b => b.UserId == userId && b.AuctionId == auctionId).CountAsync();
    }
}