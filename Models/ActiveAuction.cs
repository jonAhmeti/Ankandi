namespace Auction.Models
{
    public class ActiveAuction
    {
        public int AuctionId { get; set; }
        public bool Opened { get; set; }
        public int OpenedBy { get; set; }
        public bool Closed { get; set; }
        public int ClosedBy { get; set; }
    }
}
