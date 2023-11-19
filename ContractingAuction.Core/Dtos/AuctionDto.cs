using System.ComponentModel.DataAnnotations;
using ContractingAuction.Core.Enums;

namespace ContractingAuction.Core.Dtos;

public class AuctionDto
{
    [Required]
    public string? Title { get; set; }

    public string? Description { get; set; }

    [Required]
    public double StartPrice { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

}