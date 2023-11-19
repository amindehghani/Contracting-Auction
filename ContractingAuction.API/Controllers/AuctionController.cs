using ContractingAuction.Core.Dtos;
using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace ContractingAuction.API.Controllers;

[ApiController]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;

    public AuctionController(
        IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }
    [HttpGet]
    [Route("api/[controller]")]
    public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
    {
        return Ok(await _auctionService.GetAuctions());
    }

    [HttpPost]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Auction>> CreateAuction([FromBody] AuctionDto model)
    {
        Auction auction = await _auctionService.CreateAuction(new Auction()
        {
            Title = model.Title,
            Description = model.Description,
            EndDate = model.EndDate,
            StartPrice = model.StartPrice,
            CurrentPrice = model.StartPrice
        });
        return Ok(auction);
    }
}