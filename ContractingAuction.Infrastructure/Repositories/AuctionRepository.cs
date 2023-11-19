using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContractingAuction.Infrastructure.Repositories;

public class AuctionRepository : BaseRepository<Auction>, IAuctionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuctionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Auction>> GetEndedAuctions()
    {
        return await _dbContext.Auctions.Where(a => a.Status == AuctionStatus.Running && a.EndDate < DateTime.UtcNow)
            .ToListAsync();
    }
}