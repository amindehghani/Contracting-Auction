using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.Services;
using ContractingAuction.Infrastructure.Repositories;
using ContractingAuction.Infrastructure.Services;

namespace ContractingAuction.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection RegisterService(this IServiceCollection services)
    {
        #region Services

        services.AddScoped<IAuctionService, AuctionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBidService, BidService>();

        #endregion

        #region Repositories

        services.AddTransient<IAuctionRepository, AuctionRepository>();
        services.AddTransient<IBidRepository, BidRepository>();

        #endregion

        return services;
    }
}