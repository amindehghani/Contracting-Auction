using AutoMapper;
using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Interfaces.IMapper;
using ContractingAuction.Core.Interfaces.IRepositories;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.Mapper;
using ContractingAuction.Core.Services;
using ContractingAuction.Core.ViewModels;
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

        #region Mapper

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Auction, AuctionViewModel>();
            cfg.CreateMap<Bid, BidViewModel>();
        });

        IMapper mapper = configuration.CreateMapper();

        services.AddSingleton<IBaseMapper<Auction, AuctionViewModel>>(new BaseMapper<Auction, AuctionViewModel>(mapper));
        services.AddSingleton<IBaseMapper<Bid, BidViewModel>>(new BaseMapper<Bid, BidViewModel>(mapper));

        #endregion

        return services;
    }
}