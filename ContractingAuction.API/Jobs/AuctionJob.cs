using ContractingAuction.Core.Interfaces.IServices;
using Quartz;

namespace ContractingAuction.API.Jobs;

public class AuctionJob : IJob
{
    private IAuctionService? _auctionService;
    private ILogger<AuctionJob> _logger;

    public Task Execute(IJobExecutionContext context)
    {
        if (context.JobDetail.JobDataMap["ServiceProvider"] is not IServiceProvider serviceProvider)
        {
            return Task.FromException(new Exception("No ServiceProvider"));
        }

        _auctionService = serviceProvider.GetService<IAuctionService>()!;
        _logger = serviceProvider.GetService<ILogger<AuctionJob>>()!;

        return Task.Run(Run);
    }

    private void Run()
    {
        _logger.LogInformation("Closing Ended Auctions...");
        _auctionService!.CloseEndedAuctions();
    }
}