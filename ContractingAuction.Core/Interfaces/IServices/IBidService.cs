using ContractingAuction.Core.Entities;
using ContractingAuction.Core.ViewModels;

namespace ContractingAuction.Core.Interfaces.IServices;

public interface IBidService
{
    Task<BidViewModel> PlaceBid(Auction auction, string userId, double amount);
    Task<IEnumerable<Bid>> GetBids();
    Task<IEnumerable<Bid>> GetBids(int auctionId);
    Task<Bid?> GetLowestBid(int auctionId);
}