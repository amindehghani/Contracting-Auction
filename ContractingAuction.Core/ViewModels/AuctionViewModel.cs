using ContractingAuction.Core.Enums;

namespace ContractingAuction.Core.ViewModels;

public class AuctionViewModel
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public double CurrentPrice { get; set; }
    public DateTime EndDate { get; set; }
    public AuctionStatus Status { get; set; }

    public ICollection<BidViewModel> Bids { get; set; } = new List<BidViewModel>();
}