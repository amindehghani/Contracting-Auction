using ContractingAuction.Core.Entities;

namespace ContractingAuction.Core.Interfaces.IServices;

public interface IBidService
{
    Task<Bid> PlaceBid(int auctionId, string userId, double amount);
    Task<IEnumerable<Bid>> GetBids();
    Task<IEnumerable<Bid>> GetBids(int auctionId);
}