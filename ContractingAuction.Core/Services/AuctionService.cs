using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Core.Interfaces.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace ContractingAuction.Core.Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IServiceProvider _serviceProvider;
    private readonly IBidService _bidService;

    public AuctionService(
        IAuctionRepository auctionRepository,
        IServiceProvider serviceProvider)
    {
        _auctionRepository = auctionRepository;
        _serviceProvider = serviceProvider;
        _bidService = serviceProvider.GetService<IBidService>()!;
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

    public async Task CloseEndedAuctions()
    {
        IEnumerable<Auction> auctions = await _auctionRepository.GetEndedAuctions();
        foreach (Auction auction in auctions)
        {
            Bid? winningBid = await GetWinningBid(auction);
            if (winningBid is not null)
            {
                auction.WinnerId = winningBid.UserId;
            }
            auction.Status = AuctionStatus.Ended;
            await _auctionRepository.UpdateAsync(auction);
        }
    }

    private async Task<Bid?> GetWinningBid(Auction auction)
    {
    IEnumerable<Bid> auctionBids = await _bidService.GetBids(auction.Id);
    return auctionBids.MinBy(b => b.Amount) ?? null;
    }
}