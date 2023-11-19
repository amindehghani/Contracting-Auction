using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Core.Interfaces.IServices;

namespace ContractingAuction.Core.Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;

    public AuctionService(
        IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }
    public async Task<IEnumerable<Auction>> GetAuctions()
    {
        return await _auctionRepository.GetAllAsync();
    }

    public async Task<Auction?> GetAuction(int id)
    {
        return await _auctionRepository.GetByIdAsync(id);
    }

    public async Task<Auction> CreateAuction(Auction auction)
    {
        return await _auctionRepository.CreateAsync(auction);
    }

    public async Task UpdateAuction(Auction auction)
    {
        await _auctionRepository.UpdateAsync(auction);
    }

    public async Task DeleteAuction(int id)
    {
        Auction? auction = await GetAuction(id);
        if (auction is not null)
        {
            await _auctionRepository.DeleteAsync(auction);
        }
    }
}