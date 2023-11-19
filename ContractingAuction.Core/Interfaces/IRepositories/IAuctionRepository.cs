using ContractingAuction.Core.Entities;

namespace ContractingAuction.Core.Interfaces.IRepositories;

public interface IAuctionRepository : IBaseRepository<Auction>
{
    Task<IEnumerable<Auction>> GetEndedAuctions();
}