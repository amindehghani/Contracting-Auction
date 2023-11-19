using ContractingAuction.Core.Entities;

namespace ContractingAuction.Core.Interfaces.IRepositories;

public interface IBidRepository : IBaseRepository<Bid>
{
    Task<int> GetUserTotalBids(string userId, int auctionId);
}