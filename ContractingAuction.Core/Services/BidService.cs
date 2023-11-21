using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Exceptions;
using ContractingAuction.Core.Interfaces.IMapper;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.ViewModels;

namespace ContractingAuction.Core.Services;

public class BidService : IBidService
{
    private readonly IBidRepository _bidRepository;
    private readonly IAuctionService _auctionService;
    private readonly IBaseMapper<Bid, BidViewModel> _bidviewModelMapper;

    public BidService(
        IBidRepository bidRepository,
        IAuctionService auctionService,
        IBaseMapper<Bid, BidViewModel> bidviewModelMapper)
    {
        _bidRepository = bidRepository;
        _auctionService = auctionService;
        _bidviewModelMapper = bidviewModelMapper;
    }
    public async Task<BidViewModel> PlaceBid(Auction auction, string userId, double amount)
    {
        // check if bidding limit is reached for user
        int currentCount = await _bidRepository.GetUserTotalBids(userId, auction.Id);
        if (currentCount >= 3)
        {
            throw new BidLimitException();
        }
        
        auction.CurrentPrice = amount;
        auction.UpdateDate = DateTime.UtcNow;
        await _auctionService.UpdateAuction(auction);
        
        return _bidviewModelMapper.MapModel(await _bidRepository.CreateAsync(new Bid()
        {
            AuctionId = auction.Id,
            UserId = userId,
            Amount = amount
        }));
    }

    public async Task<IEnumerable<Bid>> GetBids()
    {
        return await _bidRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Bid>> GetBids(int auctionId)
    {
        IEnumerable<Bid> bids = await GetBids();
        return bids.Where(b => b.AuctionId == auctionId);
    }

    public async Task<Bid?> GetLowestBid(int auctionId)
    {
        return (await _bidRepository.GetAllAsync()).Where(b => b.AuctionId == auctionId).MinBy(b => b.Amount);
    }
}