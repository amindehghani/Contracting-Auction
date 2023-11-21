using ContractingAuction.Core.Entities;
using ContractingAuction.Core.ViewModels;

namespace ContractingAuction.Core.Interfaces.IServices;

public interface IAuctionService
{
    Task<IEnumerable<AuctionViewModel>> GetAuctions();
    Task<Auction?> GetAuction(int id);
    Task<Auction> CreateAuction(Auction auction);
    Task UpdateAuction(Auction auction);
    Task DeleteAuction(int id);

    Task<IEnumerable<Auction>> GetEndedAuctions();
}