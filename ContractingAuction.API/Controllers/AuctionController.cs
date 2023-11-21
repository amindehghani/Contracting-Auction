using ContractingAuction.Core.Dtos;
using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace ContractingAuction.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionController : ControllerBase
{
    private readonly IAuctionService _auctionService;

    public AuctionController(
        IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuctionViewModel>>> GetAuctions()
    {
        return Ok(await _auctionService.GetAuctions());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Auction>> GetAuction(int id)
    {
        Auction? auction = await _auctionService.GetAuction(id);
        if (auction is null)
        {
            return NotFound();
        }

        return Ok(auction);
    }

    [HttpPost]
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