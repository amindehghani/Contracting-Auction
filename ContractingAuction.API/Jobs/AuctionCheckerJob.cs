using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Interfaces.IServices;

namespace ContractingAuction.API.Jobs;

public class AuctionCheckerJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AuctionCheckerJob> _logger;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(20));
    
    
    public AuctionCheckerJob(
        IServiceProvider serviceProvider,
        ILogger<AuctionCheckerJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            await DoWorkAsync();
        }
    }

    private async Task DoWorkAsync()
    {
        _logger.LogInformation("Closing Ended Auctions...");
        using IServiceScope scope = _serviceProvider.CreateScope();
        IAuctionService auctionService = scope.ServiceProvider.GetRequiredService<IAuctionService>();
        IBidService bidService = scope.ServiceProvider.GetRequiredService<IBidService>();
        IEnumerable<Auction> endedAuctions = await auctionService.GetEndedAuctions();
        foreach (Auction auction in endedAuctions)
        {
            Bid? lowestBid = await bidService.GetLowestBid(auction.Id);
            if (lowestBid is not null)
            {
                auction.WinnerId = lowestBid.UserId;
            }

            auction.Status = AuctionStatus.Ended;
            await auctionService.UpdateAuction(auction);
        }
        
        _logger.LogInformation($"{endedAuctions.Count()} Auctions Closed.");
    }
}