using System.Security.Claims;
using ContractingAuction.Core.Dtos;
using ContractingAuction.Core.Entities;
using ContractingAuction.Core.Exceptions;
using ContractingAuction.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractingAuction.API.Controllers;

[ApiController]
public class BidController : ControllerBase
{
    private readonly IBidService _bidService;

    public BidController(
        IBidService bidService)
    {
        _bidService = bidService;
    }

    [Authorize]
    [HttpPost]
    [Route("api/[controller]/place")]
    public async Task<ActionResult<Bid>> PlaceBid([FromBody] PlaceBidDto model)
    {
        try
        {
            return await _bidService.PlaceBid(model.AuctionId, User!.FindFirst("userId")?.Value ?? "",
                model.Amount);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
        
    }
}