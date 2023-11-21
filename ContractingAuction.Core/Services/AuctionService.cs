using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Interfaces.IMapper;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ContractingAuction.Core.Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IBaseMapper<Auction, AuctionViewModel> _auctionViewModelMapper;

    public AuctionService(
        IAuctionRepository auctionRepository,
        IBaseMapper<Auction, AuctionViewModel> auctionViewModelMapper)
    {
        _auctionRepository = auctionRepository;
        _auctionViewModelMapper = auctionViewModelMapper;
    }
    public async Task<IEnumerable<AuctionViewModel>> GetAuctions()
    {
        return _auctionViewModelMapper.MapList(await _auctionRepository.GetAllAsync());
    }

    public async Task<AuctionViewModel?> GetAuction(int id)
    {
        Auction? auction = await _auctionRepository.GetByIdAsync(id);
        return auction is null ? null : _auctionViewModelMapper.MapModel(auction);
    }

    public async Task<AuctionViewModel> CreateAuction(Auction auction)
    {
        return _auctionViewModelMapper.MapModel(await _auctionRepository.CreateAsync(auction));
    }

    public async Task UpdateAuction(Auction auction)
    {
        await _auctionRepository.UpdateAsync(auction);
    }

    public async Task DeleteAuction(int id)
    {
        Auction? auction = await _auctionRepository.GetByIdAsync(id);
        if (auction is not null)
        {
            await _auctionRepository.DeleteAsync(auction);
        }
    }

    public async Task<IEnumerable<Auction>> GetEndedAuctions()
    {
        return await _auctionRepository.GetEndedAuctions();
        // foreach (Auction auction in auctions)
        // {
            // int l = 0;
            // Bid? winningBid = await GetWinningBid(auction);
            // if (winningBid is not null)
            // {
            // auction.WinnerId = winningBid.UserId;
            // }
            // auction.Status = AuctionStatus.Ended;
            // await _auctionRepository.UpdateAsync(auction);
        // }
    }
}