using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContractingAuction.Core.Enums;

namespace ContractingAuction.Core.Entities;

[Table("Auctions")]
public class Auction : Base
{
    [Required]
    public string? Title { get; set; }

    public string? Description { get; set; }

    [Required]
    public double StartPrice { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public AuctionStatus Status { get; set; }

    public double CurrentPrice { get; set; }
    public string? WinnerId { get; set; }

    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
}