using Microsoft.AspNetCore.Identity;

namespace ContractingAuction.Core.Entities;

public class Bid : Base
{
    public double Amount { get; set; }
    
    public int AuctionId { get; set; }
    public Auction? Auction { get; set; }

    public string UserId { get; set; } = "";
    public IdentityUser? User { get; set; }
}