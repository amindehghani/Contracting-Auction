using System.Security.Claims;
using ContractingAuction.Core.Dtos;
using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Enums;
using ContractingAuction.Core.Exceptions;
using ContractingAuction.Core.Interfaces.IServices;
using ContractingAuction.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractingAuction.API.Controllers;

[ApiController]
public class BidController : ControllerBase
{
    private readonly IBidService _bidService;
    private readonly IAuctionService _auctionService;

    public BidController(
        IBidService bidService,
        IAuctionService auctionService)
    {
        _bidService = bidService;
        _auctionService = auctionService;
    }

    [Authorize]
    [HttpPost]
    [Route("api/[controller]/place")]
    public async Task<ActionResult<BidViewModel>> PlaceBid([FromBody] PlaceBidDto model)
    {
        Auction? auction = await _auctionService.GetAuction(model.AuctionId);
        if (auction is null)
        {
            return NotFound();
        }

        if (auction.Status != AuctionStatus.Running || auction.EndDate < DateTime.UtcNow)
        {
            return BadRequest("The Auction is Ended and you cannot bid right now.");
        }

        if (auction.CurrentPrice <= model.Amount)
        {
            return BadRequest("Entered price is higher or equal to current price.");
        }
        
        try
        {
            return await _bidService.PlaceBid(auction, User!.FindFirst("userId")?.Value ?? "",
                model.Amount);
        }
        catch (BidLimitException ex)
        {
            return BadRequest("Bid Limit of 3 is reached for you!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}