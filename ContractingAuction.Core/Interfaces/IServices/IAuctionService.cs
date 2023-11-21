using ContractingAuction.Core.Entities;
using ContractingAuction.Core.ViewModels;

namespace ContractingAuction.Core.Interfaces.IServices;

public interface IAuctionService
{
    Task<IEnumerable<AuctionViewModel>> GetAuctions();
    Task<AuctionViewModel?> GetAuction(int id);
    Task<AuctionViewModel> CreateAuction(Auction auction);
    Task UpdateAuction(Auction auction);
    Task DeleteAuction(int id);

    Task<IEnumerable<Auction>> GetEndedAuctions();
}