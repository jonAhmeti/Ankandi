namespace Auction.Models
{
    public class ActiveAuctions
    {
        public int AuctionId { get; set; }
        public bool Open { get; set; }
        public int OpenedBy { get; set; }
    }
}
