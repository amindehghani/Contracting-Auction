using ContractingAuction.Core.Interfaces.IServices;

namespace ContractingAuction.API.Jobs;

public class AuctionCloserService: BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AuctionCloserService(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await CloseAuctions();
    }

    private async Task CloseAuctions()
    {
        using var scope = _serviceProvider.CreateScope();
        var auctionService = scope.ServiceProvider.GetService<IAuctionService>();
        await auctionService.CloseEndedAuctions();
    }
}