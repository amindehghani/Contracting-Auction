using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Exceptions;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Core.Interfaces.IServices;

namespace ContractingAuction.Core.Services;

public class BidService : IBidService
{
    private readonly IBidRepository _bidRepository;
    private readonly IAuctionService _auctionService;

    public BidService(
        IBidRepository bidRepository,
        IAuctionService auctionService)
    {
        _bidRepository = bidRepository;
        _auctionService = auctionService;
    }
    public async Task<Bid> PlaceBid(int auctionId, string userId, double amount)
    {
        Auction? auction = await _auctionService.GetAuction(auctionId);
        if (auction is null)
        {
            throw new NotFoundException("Auction not Found");
        }
        // check if bidding limit is reached for user
        int currentCount = await _bidRepository.GetUserTotalBids(userId, auctionId);
        if (currentCount >= 3)
        {
            throw new BidLimitException();
        }

        if (auction.CurrentPrice <= amount)
        {
            throw new InvalidPriceException("Entered price is higher or equal to current price.");
        }
        
        auction.CurrentPrice = amount;
        auction.UpdateDate = DateTime.UtcNow;
        await _auctionService.UpdateAuction(auction);
        
        return await _bidRepository.CreateAsync(new Bid()
        {
            AuctionId = auctionId,
            UserId = userId,
            Amount = amount
        });
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
}