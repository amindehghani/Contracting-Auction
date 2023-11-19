using ContractingAuction.Core.Entities;

namespace ContractingAuction.Core.Interfaces.IServices;

public interface IAuctionService
{
    Task<IEnumerable<Auction>> GetAuctions();
    Task<Auction?> GetAuction(int id);
    Task<Auction> CreateAuction(Auction auction);
    Task UpdateAuction(Auction auction);
    Task DeleteAuction(int id);

    Task CloseEndedAuctions();
}